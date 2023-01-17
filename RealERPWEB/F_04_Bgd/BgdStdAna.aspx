<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="BgdStdAna.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.BgdStdAna" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .style101 {
            border-style: none;
        }
    </style>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gvAnalysis = $('#<%=this.gvAnalysis.ClientID%>');
            gvAnalysis.Scrollable();
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
            <div class="card mt-2 pb-2">
                <div class="card-header">
                    <div class="row mt-3">
                        <div class="col-md-1" style="margin-top: 20px;">
                            <asp:TextBox ID="txtItemSearch" AutoCompleteType="Disabled" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                        </div>
                        <div class="col-md-0" style="margin-top: 20px;">
                            <asp:LinkButton ID="ImgbtnFindItem" CssClass="btn btn-sm btn-primary" runat="server" OnClick="ImgbtnFindItem_Click"><span class="fa fa-search"> </span></asp:LinkButton>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblItem" runat="server" Font-Size="11px" CssClass="form-label">Description of Items</asp:Label>
                            <asp:DropDownList ID="ddlItem" runat="server" CssClass="chzn-select form-control form-control-sm">
                            </asp:DropDownList>
                            <asp:Label ID="lblItemDesc" runat="server" Visible="false" CssClass="form-control form-control-sm "></asp:Label>
                        </div>
                        <div class="col-md-1 ml-2" style="margin-top: 20px;">
                            <asp:LinkButton ID="lbtnOk1" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk1_Click" Text="Select Item"></asp:LinkButton>
                        </div>
                        <div class="col-md-1" style="margin-top: 20px;">
                            <asp:HyperLink ID="hlbtAddnew1" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook?InputType=Wrkschedule" CssClass="btn btn-xs btn-success" ToolTip="Add New Work" BackColor="transparent"><span class="fa fa-plus" aria-hidden="true" style="color:blue"></span></asp:HyperLink>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblItem3" runat="server" CssClass="smLbl_to" Text="Std Qty"></asp:Label>
                            <asp:Label ID="lblStdQtyF" runat="server" Text=" " CssClass="form-control form-control-sm"></asp:Label>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblItem8" runat="server" CssClass="smLbl_to" Text="Unit"></asp:Label>
                            <asp:Label ID="lblUnitFPS" runat="server" CssClass="form-control form-control-sm" Text=" "></asp:Label>
                        </div>
                        <div class="col-md-2" style="position: relative">
                            <div style="position: absolute; top: 0; left: 0">
                                <asp:Label ID="lblFloor1" runat="server" CssClass="form-label" Text="Catagory:"></asp:Label>
                                <asp:DropDownList ID="ddlFloor1" runat="server" Font-Bold="True" CssClass="form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 20px;">
                            <asp:CheckBox ID="ChkCopy" runat="server" AutoPostBack="True" OnCheckedChanged="ChkCopy_CheckedChanged" Text="Copy"
                                Visible="False" CssClass="btn btn-sm btn-primary chkBoxControl primaryBtn" />
                            <asp:Label ID="lblItemDes2" runat="server" CssClass="form-label" Style="display: none;"></asp:Label>
                        </div>
                    </div>
                </div>
                
                <div class="card-body">
                    <asp:Panel ID="PnlAnalysis" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-md-1" style="margin-top: 20px;">
                                <asp:TextBox ID="txtResSearch" AutoCompleteType="Disabled" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                            </div>
                            <div class="col-md-0" style="margin-top: 20px;">
                                <asp:LinkButton ID="ImgbtnFindResource" CssClass="btn btn-sm btn-primary" runat="server" OnClick="ImgbtnFindResource_Click" TabIndex="2"><span class="fa fa-search"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label2" runat="server" Font-Size="11px" CssClass="form-label">Desc of Resources</asp:Label>
                                <asp:DropDownList ID="ddlResource" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlResource_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1" style="margin-top: 22px;">
                                <asp:LinkButton ID="lbtnOk2" runat="server" CssClass="btn btn-sm btn-primary primaryBtn" AutoPostBack="True" OnClick="lbtnOk2_Click">Select Res</asp:LinkButton>
                            </div>
                            <div class="col-md-1" style="margin-top: 22px;">
                                <asp:HyperLink ID="hlbtAddnew2" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook?InputType=MatLab" CssClass="btn btn-xs btn-success" ToolTip="Add New Resource" BackColor="transparent"><span class="fa fa-plus" aria-hidden="true" style="color:blue"></span></asp:HyperLink>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label5" runat="server" CssClass="smLbl_to" Text="Current Column Group:"></asp:Label>
                                <asp:Label ID="lblColGroup" runat="server" Text=" " CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                            <div class="col-md-2" style="margin-top: 20px;">
                                <table>
                                    <tr>
                                        <td class="style43">
                                            <asp:LinkButton ID="lbtngvP1" runat="server" Font-Bold="True" Font-Size="12px"
                                                OnClick="lbtngvP_Click" Style="text-align: center" Width="17px"
                                                Height="16px">1</asp:LinkButton>
                                        </td>
                                        <td class="style17">
                                            <asp:LinkButton ID="lbtngvP2" runat="server" Font-Bold="True" Font-Size="12px"
                                                OnClick="lbtngvP_Click" Style="text-align: center" Width="17px">2</asp:LinkButton>
                                        </td>
                                        <td class="style34">
                                            <asp:LinkButton ID="lbtngvP3" runat="server" Font-Bold="True" Font-Size="12px"
                                                OnClick="lbtngvP_Click" Style="text-align: center" Width="17px">3</asp:LinkButton>
                                        </td>
                                        <td class="style114">
                                            <asp:LinkButton ID="lbtngvP4" runat="server" Font-Bold="True" Font-Size="12px"
                                                OnClick="lbtngvP_Click" Style="text-align: center" Width="17px">4</asp:LinkButton>
                                        </td>
                                        <td class="style115">
                                            <asp:LinkButton ID="lbtngvP5" runat="server" Font-Bold="True" Font-Size="12px"
                                                OnClick="lbtngvP_Click" Style="text-align: center" Width="17px">5</asp:LinkButton>
                                        </td>
                                        <td class="style116">
                                            <asp:LinkButton ID="lbtngvP6" runat="server" Font-Bold="True" Font-Size="12px"
                                                OnClick="lbtngvP_Click" Style="text-align: center" Width="17px">6</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbtngvP7" runat="server" Font-Bold="True" Font-Size="12px"
                                                OnClick="lbtngvP_Click" Style="text-align: center" Width="17px">7</asp:LinkButton>
                                        </td>
                                        <td class="style113">
                                            <asp:LinkButton ID="lbtngvP8" runat="server" Font-Bold="True" Font-Size="12px"
                                                OnClick="lbtngvP_Click" Style="text-align: center" Width="17px">8</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row mt-2" style="margin-top: 20px;">
                            <div class="col-md-1" style="margin-top: 20px;">
                                <asp:CheckBox ID="ChkZeroQty" runat="server" Text="Ignoe Zero" CssClass="btn btn-sm btn-primary primaryBtn chkBoxControl" />
                            </div>
                            <div class="col-md-6" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnInputSame" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnInputSame_Click" Style="padding-left: 5px; padding-right: 5px">Put same value for all floors</asp:LinkButton>
                                <asp:LinkButton ID="lbtnUpdateAna" runat="server" class="btn btn-sm btn-danger" OnClick="lbtnUpdateAna_Click" Style="margin-left: 5px;">Update Analysis</asp:LinkButton>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <asp:GridView ID="gvAnalysis" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                Width="16px" OnRowDeleting="gvAnalysis_RowDeleting" HeaderStyle-CssClass="HeaderStyle" ShowFooter="true">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtngvSlNo" runat="server" Font-Bold="True"
                                                Font-Size="12px" OnClick="lbtngvSlNo_Click" Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:CommandField ShowDeleteButton="True" />  OnCheckedChanged="chkvmrno_CheckedChanged"--%>

                                    <asp:TemplateField ShowHeader="true">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDelAnalysis" runat="server" OnClick="lbtnDelAnalysis_Click" ToolTip="Delete Analysis" OnClientClick="javascript:return FunConfirm();"><i class="fa fa-trash" style="color:red"> </i> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllCheckid" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllCheckid_CheckedChanged" Width="30px" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkvmrno" runat="server" AutoPostBack="false" Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv")) == "True" ? false : true %>'
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                                Width="30px" CssClass="btn btn-default btn-xs" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnChekedId" runat="server" OnClientClick="return FunAppConfirm();" OnClick="lnkbtnChekedId_Click" ToolTip="Delete"><span class=" fa fa-check "></span>
                                            </asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Res. Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResCod" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description of Resources">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResDesc" runat="server" Font-Bold="True"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Init.Work">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty001" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty001")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mo/biliz.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty002" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty002")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SubStruc.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty003" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty003")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Base-1">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty004" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty004")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Base-2">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty005" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty005")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Base-3">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty006" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty006")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Base-4">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty007" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty007")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Base-5">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty008" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty008")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Base-6">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty009" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty009")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Base-7">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty010" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty010")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Gr. Floor">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty011" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty011")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Mz. Floor-1" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty012" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty012")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mz. Floor-2" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty013" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty013")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="1st Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty014" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty014")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="2nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty015" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty015")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="3rd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty016" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty016")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="4th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty017" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty017")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="5th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty018" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty018")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="6th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty019" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty019")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="7th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty020" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty020")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="8th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty021" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty021")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="9th floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty022" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty022")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="10th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty023" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty023")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="11th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty024" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty024")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="12th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty025" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty025")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="13th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty026" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty026")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="14th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty027" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty027")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="15th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty028" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty028")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="16th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty029" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty029")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="17th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty030" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty030")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="18th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty031" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty031")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="19th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty032" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty032")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="20th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty033" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty033")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="21st Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty034" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty034")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="22ndFloor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty035" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty035")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="23rd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty036" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty036")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="24th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty037" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty037")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="25th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty038" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty038")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="26th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty039" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty039")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="27th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty040" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty040")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="28th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty041" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty041")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="29th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty042" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty042")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="30th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty043" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty043")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="31st Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty044" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty044")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="32nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty045" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty045")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="33nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty046" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty046")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="34th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty047" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty047")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="35th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty048" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty048")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="36nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty049" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty049")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="37th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty050" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty050")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="38th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty051" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty051")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Roof Top" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty052" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty052")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mezz.Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty053" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty053")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comm.Work" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty054" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty054")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
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

                        <div class="row" style="margin-bottom:90px;">
                            <div class="col-md-1"></div>
                            <div class="col-md-1">
                                <asp:TextBox ID="txtItemSearchToCopy" AutoCompleteType="Disabled" Visible="false" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                            </div>
                            <div class="col-md-0">
                                <asp:LinkButton ID="ImgbtnFindItemToCopy" CssClass="btn btn-primary btn-sm" Visible="false" runat="server" OnClick="ImgbtnFindItemToCopy_Click" TabIndex="2"><span class="fa fa-search"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlItemToCopy" runat="server" CssClass="form-control form-control-sm chzn-select" Visible="false">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1 pading5px">
                                <asp:LinkButton ID="lbtnCopyData" runat="server" CssClass="btn btn-primary primaryBtn" Visible="false"
                                    OnClick="lbtnCopyData_Click">Copy Data</asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


