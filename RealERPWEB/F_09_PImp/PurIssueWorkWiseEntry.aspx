<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PurIssueWorkWiseEntry.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurIssueWorkWiseEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .mt20 {
            margin-top: 20px !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .chzn-container {
            width: 100% !important;
        }
    </style>
    <script type="text/javascript">

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
                            <div class="loading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>


            <div class="card mt-4">
                <div class="card-header">
            <div class="form-group row">
                <div class="col-lg-3 ">
                    <asp:Label ID="lblProjectList" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                    <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True"></asp:DropDownList>
                    <asp:Label ID="lblddlProject" runat="server" Visible="False" CssClass="  "></asp:Label>
                </div>
                <div class="col-lg-1 ">
                    <asp:Label ID="Label9" runat="server" Text="Ref. No."></asp:Label>
                    <asp:TextBox ID="txtBillno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                </div>
                <%--            <div class="col-md-3 ">
                    <div class="msgHandSt">
                        <asp:Label ID="lblmsg1" CssClass="btn btn-danger primaryBtn" runat="server" Visible="false"></asp:Label>
                    </div>

                </div>--%>

                <div class="col-lg-2">
                    <asp:Label ID="lblIssuno" runat="server" Text="Issue No:"></asp:Label>
                    <div class="row">
                        <asp:Label ID="lblCurISSNo1" runat="server" CssClass="col-lg-6 form-control form-control-sm">WEN</asp:Label>
                        <asp:TextBox ID="txtCurISSNo2" runat="server" CssClass="col-lg-6 form-control form-control-sm" >000</asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-1">
                    <asp:Label ID="Label7" runat="server"  Text="Date"></asp:Label>
                    <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server"
                        Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>
           
                </div>

                <div class="col-lg-2">
                    <asp:Label ID="Label1" runat="server">
                        <asp:LinkButton ID="lbtnPrevISSList" runat="server" OnClick="lbtnPrevISSList_Click">Previous Issue</asp:LinkButton>
                    </asp:Label>
                    <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="form-control form-control-sm chzn-select" >
                    </asp:DropDownList>
                </div>
                 <div class="col-lg-1">
                             <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click" TabIndex="11">Ok</asp:LinkButton>
                </div>
            </div>

        </div>
            </div>

            <asp:Panel ID="pnlgrd" runat="server" Visible="False">
                <div class="card">
                    <div class="card-header">
                    <asp:Panel ID="Panel3" runat="server">
                                <div class="form-group row">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblCatagory" runat="server" Text="Catagory"></asp:Label>
                                        <asp:DropDownList ID="ddlcatagory" OnSelectedIndexChanged="ddlcatagory_OnSelectedIndexChanged" runat="server" CssClass="chzn-select form-control-sm form-control"  AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblitemList" runat="server" Text="Item List"></asp:Label>
                                        <asp:DropDownList ID="ddlitemlist" runat="server" CssClass="chzn-select  form-control form-control-sm"  AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlitemlist_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                   <div class="col-lg-2">
                                      <asp:Label ID="lblfloorno" runat="server" Text="Division"></asp:Label>
                                    <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" CssClass="form-control form-control-sm" 
                                        MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True" Width="200px">
                                    </cc1:DropCheck>
                                    </div>

                                    <div class="col-lg-1">
                                           <asp:LinkButton ID="LinkButton1" runat="server"  OnClick="lbtnSelect_Click" CssClass="btn btn-primary btn-sm mt20"
                                        TabIndex="17">Select</asp:LinkButton>
                                    </div>
                                 
                                    <div class="col-lg-2 ">
                                        <asp:Label ID="lblPage" runat="server"  Text="Page"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"  OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" >
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

                  

                    </asp:Panel>
                    </div>

                    <div class="card-body">
                        <div class="row">
                    <asp:GridView ID="grvissue" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" ShowFooter="True" Width="599px"
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
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="lnkTotal_Click">Total</asp:LinkButton>
                                </FooterTemplate>

                                <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bal.Qty">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger btn-sm"
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
                                        BorderStyle="Solid" BorderWidth="1px" Style="text-align: right" CssClass="form-control form-control-sm"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>

                        <FooterStyle CssClass="grvFooterNew" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeaderNew" />

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
                                    <asp:TextBox ID="txtISSNarr" runat="server" CssClass="form-control mt-2" TextMode="MultiLine"
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
                    </div>
                </div>
       
                

            </asp:Panel>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

