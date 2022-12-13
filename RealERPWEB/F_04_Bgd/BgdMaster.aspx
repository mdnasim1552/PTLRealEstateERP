<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="BgdMaster.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.BgdMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            try {


                //var gridViewScroll = new GridViewScroll({
                //    elementID: "gvRes",
                //    width: 1000,
                //    height: 500,
                //    freezeColumn: true,
                //    freezeFooter: true,
                //    freezeColumnCssClass: "GridViewScrollItemFreeze",
                //    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                //    freezeHeaderRowCount: 2,
                //    freezeColumnCount: 12,

                //});
                //gridViewScroll.enhance();


                var dgvAccRec02 = $('#<%=this.gvRes.ClientID %>');
                dgvAccRec02.Scrollable();

                //dgvAccRec02.gridviewScroll({
                //    width: 1160,
                //    height: 420,
                //    arrowsize: 30,
                //    railsize: 16,
                //    barsize: 8,
                //    varrowtopimg: "../Image/arrowvt.png",
                //    varrowbottomimg: "../Image/arrowvb.png",
                //    harrowleftimg: "../Image/arrowhl.png",
                //    harrowrightimg: "../Image/arrowhr.png",
                //    freezesize: 6
                //});


                $('.chzn-select').chosen({ search_contains: true });
            }
            catch (e) {

                alert(e);

            }

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
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 pading5px">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend ">
                                        <span class="input-group-text">Budget Date</span>
                                    </div>
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 pading5px">
                            <div class="form-group">
                                <asp:RadioButtonList ID="rblbudgt" runat="server" AutoPostBack="True" CssClass="rbtnList1 chkBoxControl" OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" RepeatColumns="6"
                                    RepeatDirection="Horizontal" Width="250px">
                                    <asp:ListItem Value="Master" Selected="True">Master Budget</asp:ListItem>
                                    <asp:ListItem Value="Details">Details Budget</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-md-4 pading5px">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlAccountDesc" Style="margin-left: -10px;" runat="server" Visible="False" CssClass="form-control chzn-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="row">
                                <div class="col-md-3 pading5px">
                                    <div class="form-group">
                                        <div class="input-group input-group-alt">
                                            <div class="input-group-prepend ">
                                                <span class="input-group-text">Accounts Code</span>
                                            </div>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:LinkButton ID="ImageButton1" CssClass="btn btn-secondary " runat="server" OnClick="ImageButton1_Click" TabIndex="2"><i class="fa fa-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <div class="input-group input-group-alt">
                                            <div class="input-group-prepend ">
                                                <span class="input-group-text">Page</span>
                                            </div>
                                            <asp:DropDownList ID="ddlpagesize2" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize2_SelectedIndexChanged">
                                                <asp:ListItem>10</asp:ListItem>
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
                            </div>
                            <div>
                                <asp:GridView ID="gvAcc" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-bordered grvContentarea"
                                    PageSize="15" ShowFooter="True" Width="700px" OnRowDataBound="gvAcc_RowDataBound" OnPageIndexChanging="gvAcc_PageIndexChanging">
                                    <PagerSettings Visible="true" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ActCode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Accounts">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccDesc" runat="server" Width="400px" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:LinkButton ID="LnkfTotal" runat="server" OnClick="LnkfTotal_Click" Style="color: white; font-weight: bold;">Total :</asp:LinkButton>
                                            </FooterTemplate>

                                            <%-- <FooterTemplate>
                                                <asp:DropDownList ID="ddlPageNo" runat="server" __designer:wfdid="w67" AutoPostBack="True"
                                                    Font-Bold="True" Font-Size="14px" OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                                    Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                    Width="250px">
                                                </asp:DropDownList>
                                            </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderText="Level" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="gvlnkLevel" runat="server" OnClick="gvlnkLevel_Click"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dr. Amount" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" Height="22px" Style="text-align: right; color: white; font-weight: bold;" ReadOnly="true"
                                                    Width="100px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr. Amount" ItemStyle-HorizontalAlign="Right" Visible="false">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right; color: white; font-weight: bold;" Height="22px" ReadOnly="true"
                                                    Width="80px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRmRk" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" BorderWidth="1px" CssClass="ddl" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bgdrmrk")) %>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-success btn-sm form-control" OnClick="lnkFinalUpdate_Click">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="row">
                                <div class="col-md-3 pading5px">
                                    <div class="form-group">
                                        <label class="control-label  lblmargin-top9px" for="Label1">Account Head</label>
                                        <asp:Label class="control-label  lblmargin-top9px" ID="lblAccHead" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <div class="input-group input-group-alt">
                                            <div class="input-group-prepend ">
                                                <span class="input-group-text">Page</span>
                                            </div>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                                <asp:ListItem>10</asp:ListItem>
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
                                <div class="col-md-1 pull-right">
                                    <div class="form-group">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkHome_Click" CssClass="btn btn-primary primaryBtn">Home</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="Panel2" runat="server">
                                <div class="row">
                                    <div class="col-md-3 pading5px">
                                        <div class="form-group">
                                            <div class="input-group input-group-alt">
                                                <div class="input-group-prepend ">
                                                    <span class="input-group-text">Resource Code</span>
                                                </div>
                                                <asp:TextBox ID="txtResSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                <div class="input-group-append">
                                                    <asp:LinkButton ID="ibtnDetailsCode" CssClass="btn btn-secondary " runat="server" OnClick="ibtnDetailsCode_Click" TabIndex="2"><i class="fa fa-search"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:CheckBox ID="ChkCopyProject" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="ChkCopyProject_CheckedChanged" Text="Copy" CssClass="btn btn-primary primaryBtn chkBoxControl" />
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="PnlCopyProject" runat="server" Visible="False">
                                    <div class="row">
                                        <div class="col-md-2 pading5px">
                                            <div class="form-group">
                                                <div class="input-group input-group-alt">
                                                    <div class="input-group-prepend ">
                                                        <span class="input-group-text">From Project</span>
                                                    </div>
                                                    <asp:TextBox ID="txtSrcCopyPro" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <div class="input-group-append">
                                                        <asp:LinkButton ID="ibtnCopyFindProject" CssClass="btn btn-secondary " runat="server" OnClick="ibtnCopyFindProject_Click" TabIndex="2"><i class="fa fa-search"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCopyProjectName" runat="server" CssClass="form-control chzn-select inputTxt" TabIndex="3">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lbtnCopyProject" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnCopyProject_Click" TabIndex="4">Copy</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                            <div class="clearfix"></div>
                            <div class="row">
                                <asp:GridView ID="gvRes" ClientIDMode="Static" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    BackColor="#F0F0F0" PageSize="15" ShowFooter="True" Width="700px"
                                    CssClass=" table-striped  table-bordered grvContentarea"
                                    OnPageIndexChanging="gvRes_PageIndexChanging" OnRowDeleting="gvRes_RowDeleting">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="main Rescode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmrescode" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msircode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Rescode">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblrescode" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Description of Subsidiary Accounts">
                                            <HeaderTemplate>
                                                <table style="width: 350px;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Description of Subsidiary Accounts" Width="180px"></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                                CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><i class="fa fa-file-excel"></i></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblResDesc" runat="server" Font-Size="12px"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "msirdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "msirdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                          
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "")   %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:CheckBox ID="chklkrate" runat="server" AutoPostBack="True" OnCheckedChanged="chklkrate_CheckedChanged"
                                                    Text="Lock" Font-Bold="True" Font-Italic="False" Font-Size="12px" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Specification">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpcfDesc" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblresunit" runat="server" CssClass="GridLebel" Width="50px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Dhag No" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtRmRk" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" BorderWidth="1px" CssClass="ddl" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bgdrmrk")) %>'
                                                    Width="120px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtQty" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px" Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click"
                                                    CssClass="btn btn-success btn-sm form-control">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtRate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right;"
                                                    ReadOnly='<%# Convert.ToBoolean((this.GetCompCode()=="3101"||this.GetCompCode()=="3367" || this.GetCompCode()=="3368") ? false:true ) %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="gvlnkFTotal" runat="server" Style="color: white; font-weight: bold;"
                                                    OnClick="gvlnkFTotal_Click">Total:</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px" FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Dr. Amount" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtDrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="gvtxtftDramt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" Font-Bold="true" Font-Size="12px" ReadOnly="true"
                                                    Width="100px" Style="text-align: right; color: white; font-weight: bold;"></asp:TextBox>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px" FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Cr. Amount" ItemStyle-HorizontalAlign="Right" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtCrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="gvtxtftCramt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" Font-Bold="true" Font-Size="12px" ReadOnly="true"
                                                    Width="80px" Style="text-align: right; color: white; font-weight: bold;"></asp:TextBox>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="spcfcod" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfcod" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>

                                    <FooterStyle CssClass="grvFooterNew" />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

