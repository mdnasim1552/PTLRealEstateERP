<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="TASStdAnalysis.aspx.cs" Inherits="RealERPWEB.F_07_Ten.TASStdAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<%--    <style type="text/css">
        .style12 {
            width: 133px;
        }

        .style11 {
            width: 174px;
        }

        .style13 {
            width: 457px;
        }

        .style15 {
        }

        .style17 {
            width: 12px;
        }

        .style20 {
            width: 2px;
        }

        .newStyle1 {
            border: 1px solid #99CCFF;
        }

        .style21 {
        }

        .newStyle2 {
            float: none;
        }

        .style22 {
        }

        .style25 {
        }

        .style28 {
            width: 33px;
        }

        .style29 {
            width: 38px;
        }

        .newStyle3 {
            border-style: none;
            border-width: 1px;
        }

        .style33 {
        }

        .style34 {
            width: 18px;
        }

        .style36 {
            width: 70px;
        }

        .style37 {
            width: 281px;
        }

        .newStyle4 {
            background-color: #D2D2BD;
        }

        .style43 {
            width: 27px;
        }

        .style107 {
            width: 61px;
        }

        .style111 {
            width: 120px;
        }

        .style112 {
            width: 219px;
        }

        .style113 {
            width: 99px;
        }

        .style114 {
            width: 7px;
        }

        .style115 {
            width: 8px;
        }

        .style116 {
            width: 16px;
        }

        .style118 {
        }

        .style101 {
            BACKGROUND-COLOR: transparent;
            BORDER-TOP-STYLE: none;
            BORDER-RIGHT-STYLE: none;
            BORDER-LEFT-STYLE: none;
            TEXT-ALIGN: right;
            BORDER-BOTTOM-STYLE: none;
            font-size: 11px;
        }

        .style119 {
            width: 20px;
        }

        .style120 {
            width: 227px;
        }

        .style121 {
            width: 88px;
        }

        .style123 {
            width: 78px;
        }

        .style124 {
            width: 272px;
        }

        .style125 {
            width: 37px;
        }

        .style126 {
            width: 915px;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  

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

    <style type="text/css">
        .style101 {
            border-style: none;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                 <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3" style="width: 215px;">

                                        <asp:Label ID="lblItem" runat="server" Font-Size="11px" CssClass="smLbl_to">Description of Items</asp:Label>

                                        <asp:TextBox ID="txtItemSearch" AutoCompleteType="Disabled" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindItem" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindItem_OnClick" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton> 

                                    </div>


                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlItem" runat="server" CssClass="chzn-select form-control  inputTxt">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblItemDesc" runat="server" Visible="false" CssClass="form-control inputTxt"></asp:Label>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk1" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lbtnOk1_Click">Select Item</asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3" style="width: 245px;">
                                        <asp:Label ID="lblItem3" runat="server" CssClass="smLbl_to" Text="Std Qty"></asp:Label>

                                        <asp:Label ID="lblStdQtyF" runat="server" Text=" " CssClass="inputTxt inpPixedWidth"></asp:Label>
                                        <asp:Label ID="lblItem8" runat="server" CssClass="smLbl_to" Text="Unit"></asp:Label>
                                        <asp:Label ID="lblUnitFPS" runat="server" CssClass="inputTxt inpPixedWidth"
                                            Text=" "></asp:Label>
                                        

                                    </div>
                                    <div class="col-md-2 pading5px asitCol3" style="position: relative">
                                        <div style="position: absolute; top: 0; left: 0">
                                            <asp:Label ID="lblFloor1" runat="server" CssClass=" smLbl_to"
                                                Text="Floor:"></asp:Label>
                                            <asp:DropDownList ID="ddlFloor1" runat="server" Font-Bold="True" CssClass="ddlPage">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="clearfix"></div>

                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                    <div class="col-md-1 pading5px">
                                        <asp:CheckBox ID="ChkCopy" runat="server" AutoPostBack="True" OnCheckedChanged="ChkCopy_CheckedChanged" Text="Copy"
                                            Visible="False" CssClass="btn btn-primary chkBoxControl primaryBtn" />
                                        <asp:Label ID="lblItemDes2" runat="server" CssClass="smLbl_to" Style="display: none;"></asp:Label>
                                    </div>

                                    <div class="col-md-3">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                            AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" CssClass="lblProgressBar" Text="Please wait . . . . . . ."
                                                    Width="218px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>


                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="PnlAnalysis" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3" style="width: 215px;">

                                            <asp:Label ID="Label2" runat="server" Font-Size="11px" CssClass="smLbl_to">Desc of Resources</asp:Label>

                                            <asp:TextBox ID="txtResSearch" AutoCompleteType="Disabled" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindResource" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindResource_OnClick" runat="server"  TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            <%----%>
                                        </div>


                                        <div class="col-md-3 pading5px">
                                            <asp:DropDownList ID="ddlResource" runat="server" CssClass="form-control inputTxt chzn-select" AutoPostBack="True" >
                                            </asp:DropDownList>  <%--OnSelectedIndexChanged="ddlResource_SelectedIndexChanged--%>"

                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lbtnOk2" runat="server" CssClass="btn btn-primary primaryBtn"
                                                OnClick="lbtnOk2_Click">Select Res</asp:LinkButton>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol3" style="width: 245px;">
                                            <asp:Label ID="Label5" runat="server" CssClass="smLbl_to" Text="Current Column Group:"></asp:Label>

                                            <asp:Label ID="lblColGroup" runat="server" Text=" " CssClass="inputTxt inpPixedWidth"></asp:Label>


                                        </div>
                                        <div class="col-md-2 pading5px asitCol3">
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
                                        <div class="clearfix"></div>

                                    </div>

                                    <div class="form-group">

                                        <div class="col-md-6">
                                            <asp:CheckBox ID="ChkZeroQty" runat="server"
                                                Text="Ignoe Zero" CssClass=" chkBoxControl" />
                                        </div>

                                        <div class="col-md-4">
                                            <asp:LinkButton ID="lbtnInputSame" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnInputSame_Click">Put same value for all floors</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnUpdateAna" runat="server" class="btn btn-danger primaryBtn" OnClick="lbtnUpdateAna_Click" Style="margin-left: 5px;">Update Analysis</asp:LinkButton>

                                        </div>

                                        <div class="col-md-6">


                                            <div class="clearfix"></div>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <asp:GridView ID="gvAnalysis" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                Width="16px" OnRowDeleting="gvAnalysis_RowDeleting" HeaderStyle-CssClass="HeaderStyle">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtngvSlNo" runat="server" Font-Bold="True"
                                                Font-Size="12px" OnClick="lbtngvSlNo_Click" Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField HeaderText="Res. Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResCod" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Resources">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResDesc" runat="server" Font-Bold="True"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Init.Work">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty001" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty001")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobiliz.">
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
                                    <asp:TemplateField HeaderText="Gr. Floor">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty009" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty009")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="1st Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty010" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty010")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="2nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty011" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty011")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="3rd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty012" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty012")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="4th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty013" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty013")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="5th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty014" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty014")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="6th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty015" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty015")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="7th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty016" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty016")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="8th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty017" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty017")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="9th floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty018" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty018")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="10th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty019" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty019")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="11th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty020" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty020")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="12th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty021" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty021")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="13th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty022" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty022")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="14th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty023" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty023")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="15th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty024" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty024")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="16th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty025" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty025")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="17th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty026" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty026")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="18th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty027" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty027")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="19th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty028" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty028")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="20th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty029" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty029")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="21st Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty030" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty030")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="22ndFloor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty031" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty031")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="23rd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty032" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty032")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="24th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty033" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty033")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="25th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty034" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty034")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="26th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty035" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty035")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="27th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty036" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty036")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="28th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty037" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty037")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="29th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty038" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty038")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="30th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty039" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty039")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="31st Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty040" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty040")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="32nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty041" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty041")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="33nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty042" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty042")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="34th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty043" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty043")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="35th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty044" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty044")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="36nd Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty045" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty045")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="37th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty046" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty046")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="38th Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty047" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty047")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="Roof Top" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty048" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty048")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mezz.Floor" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty049" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty049")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comm.Work" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty050" runat="server" CssClass="style101"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty050")).ToString("#,##0.0000;(#,##0.0000); ") %>'
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

                            <%-- <table style="width: 924px; margin-right: 0px">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="style33">&nbsp;</td>
                                            <td class="style34">&nbsp;</td>
                                            <td class="style37">
                                                <asp:Label ID="lblItem5" runat="server" CssClass="newStyle3" Font-Size="12px"
                                                    Font-Underline="True" Style="font-weight: 700" Text="Description of Resources"
                                                    Width="320px"></asp:Label>
                                            </td>
                                            <td class="style36">
                                               
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="style107">
                                                
                                            </td>
                                            <td class="style119">&nbsp;</td>
                                            <td class="style43">&nbsp;</td>
                                            <td class="style17">&nbsp;</td>
                                            <td class="style34">&nbsp;</td>
                                            <td class="style114">&nbsp;</td>
                                            <td class="style115">&nbsp;</td>
                                            <td class="style116">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="style113">&nbsp;</td>
                                            <td class="style113">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="style33">
                                                <asp:TextBox ID="txtResSearch" runat="server" AutoCompleteType="Disabled"
                                                    BorderStyle="Solid" BorderWidth="1px" Height="18px" Width="66px"></asp:TextBox>
                                            </td>
                                            <td class="style34">
                                                <asp:ImageButton ID="ImgbtnFindResource" runat="server" Height="19px"
                                                    ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindResource_Click"
                                                    Width="16px" />
                                            </td>
                                            <td class="style37">
                                                <asp:DropDownList ID="ddlResource" runat="server" CssClass="newStyle1"
                                                    Font-Bold="True" Font-Size="11px" Height="20px" Width="327px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style36">
                                                <asp:LinkButton ID="lbtnOk2" runat="server" Font-Bold="True" Font-Size="12px"
                                                    OnClick="lbtnOk2_Click" Style="text-align: center" Width="67px">Select Res.</asp:LinkButton>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="style107">
                                                <asp:Label ID="lblItem6" runat="server" CssClass="newStyle3" Font-Size="12px"
                                                    Font-Underline="False" Style="font-weight: 700" Text="Current Column Group:"
                                                    Width="131px"></asp:Label>
                                            </td>
                                            <td class="style119">
                                                <asp:Label ID="lblColGroup" runat="server" BackColor="#660033" Font-Size="25px"
                                                    ForeColor="White" Style="text-align: center; font-weight: 700"
                                                    Width="24px">1</asp:Label>
                                            </td>
                                            
                                            <td class="style113">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="style33" colspan="16">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="style33">&nbsp;</td>
                                            <td class="style34">&nbsp;</td>
                                            <td class="style37">
                                                
                                            </td>
                                            <td class="style36">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="style107">&nbsp;</td>
                                            <td class="style119">&nbsp;</td>
                                            <td class="style43">&nbsp;</td>
                                            <td class="style17">&nbsp;</td>
                                            <td class="style34">&nbsp;</td>
                                            <td class="style114">&nbsp;</td>
                                            <td class="style115">&nbsp;</td>
                                            <td class="style116">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="style113">&nbsp;</td>
                                            <td class="style113">&nbsp;</td>
                                        </tr>
                                    </table>--%>
                        </asp:Panel>
                    </div>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-5 pading5px">
                                        <asp:TextBox ID="txtItemSearchToCopy" AutoCompleteType="Disabled" Visible="false" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindItemToCopy" CssClass="btn btn-primary srearchBtn" Visible="false" runat="server"  TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                      <%--  OnClick="ImgbtnFindItemToCopy_Click"--%>
                                        <asp:DropDownList ID="ddlItemToCopy" runat="server" CssClass=" ddlistPull chzn-select" Visible="false">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnCopyData" runat="server" CssClass="btn btn-primary primaryBtn" Visible="false"
                                            OnClick="lbtnCopyData_Click">Copy Data</asp:LinkButton>
                                    </div>

                                    <div class="clearfix"></div>

                                </div>
                            </div>
                        </fieldset>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


