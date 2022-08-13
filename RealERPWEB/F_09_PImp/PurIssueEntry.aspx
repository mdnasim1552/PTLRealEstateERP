<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurIssueEntry.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurIssueEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblProjectList" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="chzn-select ddlistPull inputTxt" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                                        <asp:Label ID="lblddlProject" runat="server" Visible="False" CssClass=" ddlistPull inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn margin5px" OnClick="lbtnOk_Click" TabIndex="11">Ok</asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label9" runat="server" CssClass="lblTxt smLbl_to" Text="Ref. No."></asp:Label>

                                        <asp:TextBox ID="txtBillno" runat="server" CssClass="inputTxt inputDateBox" TabIndex="3"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg1" CssClass="btn btn-danger primaryBtn" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblIssuno" runat="server" CssClass="lblTxt lblName" Text="Issue No:"></asp:Label>
                                        <asp:Label ID="lblCurISSNo1" runat="server" CssClass="inputTxt inputBox50px">WEN</asp:Label>
                                        <asp:TextBox ID="txtCurISSNo2" runat="server" CssClass="inputTxt inputBox50px" TabIndex="3">000</asp:TextBox>

                                        <asp:Label ID="Label7" runat="server" CssClass=" lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>

                                        


                                    </div>
                                    <div class="col-md-3 pading5px  pull-right">
                                        <asp:LinkButton ID="lbtnPrevISSList" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevISSList_Click"
                                            TabIndex="3">Previous Issue</asp:LinkButton>

                                        <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="inputTxt" TabIndex="3" style="width:140px;">
                                        </asp:DropDownList>

                                    </div>

                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <asp:Panel ID="pnlgrd" runat="server" Visible="False">
                        <div class="row">
                            <asp:Panel ID="Panel3" runat="server">

                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">
                                        <div class="form-group">

                                            <div class="col-md-6 pading5px ">
                                                <asp:Label ID="lblfloorno" runat="server" CssClass="lblTxt lblName" Text="Floor No"></asp:Label>
                                                <asp:DropDownList ID="ddlfloorno" runat="server" CssClass="ddlistPull inputTxt" Width="120" TabIndex="12" AutoPostBack="True">
                                                </asp:DropDownList>
                                                 <asp:Label ID="lblitemList" runat="server" CssClass=" smLbl_to" Text="Item List"></asp:Label>
                                                <asp:TextBox ID="txtsrchItemName" runat="server" CssClass="inputTxt lblTxt inpPixedWidth" TabIndex="10"></asp:TextBox>
                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="imgbtnSearchItemList" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnSearchItem_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                                <asp:DropDownList ID="ddlitemlist" runat="server" CssClass="chzn-select ddlistPull inputTxt" Width="200" TabIndex="12" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                 <asp:LinkButton ID="lbtnAllLab" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnAllLab_Click">Select</asp:LinkButton>

                                                  <asp:LinkButton ID="lbtnAllLaball" runat="server" CssClass="btn btn-primary primaryBtn" Style="margin-left: 20px;" OnClick="lbtnAllLaball_Click">Select ALL</asp:LinkButton>
                                            </div>
                                          
                                            <div class="col-md-2 pading5px asitCol2">
                                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt smLbl_to" Text="Page"></asp:Label>

                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Style="margin-left: 6px;" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                    <asp:ListItem Value="150">150</asp:ListItem>
                                                    <asp:ListItem Value="200">200</asp:ListItem>
                                                    <asp:ListItem Value="300">300</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>


                                        </div>

                                    </div>
                                </fieldset>

                            </asp:Panel>
                        </div>
                        <div class="row">
                            <asp:GridView ID="grvissue" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="599px" OnRowDataBound="grvissue_RowDataBound"
                                OnRowDeleting="grvissue_RowDeleting" OnPageIndexChanging="grvissue_PageIndexChanging">
                                <PagerSettings Position="TopAndBottom" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField HeaderText="Item Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fl" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvflrCode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Floor Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvflrDes" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Work Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblwrkdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "workitem")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="Label14" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkunit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    
                                     <FooterTemplate>
                                            <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary  primarygrdBtn"
                                                OnClick="lnkTotal_Click">Total</asp:LinkButton>
                                        </FooterTemplate>
                                    
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Bal.Qty">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger primaryBtn"
                                                OnClick="lnkupdate_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblbalqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Wrk.Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtwrkqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" BackColor="Transparent" BorderColor="#660033"
                                                BorderStyle="Solid" BorderWidth="1px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>
                        </div>

                        <div class="row">
                            <asp:Panel ID="Panel2" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33">
                                            <asp:Label ID="lblReqNarr" runat="server" Font-Bold="True" Font-Size="12px"
                                                Height="16px" Style="text-align: right" Text="Narration:" Width="80px"></asp:Label>
                                        </td>
                                        <td class="style34">
                                            <asp:TextBox ID="txtISSNarr" runat="server" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Bold="True" Font-Size="12px" TextMode="MultiLine"
                                                Width="416px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td class="style35">&nbsp;</td>
                                        <td class="style36">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33">
                                            <asp:Label ID="lblPreparedBy" runat="server" Font-Bold="True" Font-Size="12px"
                                                Height="16px" Style="text-align: right" Text="Prepared By:" Visible="False"
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td class="style34">
                                            <asp:TextBox ID="txtPreparedBy" runat="server" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Bold="True" Font-Size="12px" Visible="False"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblApprovedBy" runat="server" Font-Bold="True" Font-Size="12px"
                                                Height="16px" Style="text-align: right" Text="Approved By:" Visible="False"
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td class="style35">
                                            <asp:TextBox ID="txtApprovedBy" runat="server" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Bold="True" Font-Size="12px" Visible="False"
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td class="style36">
                                            <asp:Label ID="lblApprovalDate" runat="server" Font-Bold="True"
                                                Font-Size="12px" Height="16px" Style="text-align: right" Text="Approv.Date:"
                                                Visible="False" Width="66px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtApprovalDate" runat="server" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)"
                                                Visible="False" Width="100px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                    </asp:Panel>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


