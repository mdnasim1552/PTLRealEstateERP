
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProjReport02.aspx.cs" Inherits="RealERPWEB.F_32_Mis.ProjReport02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

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

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="As on Date"></asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Calfr" runat="server" Format="dd-MMM-yyyy " TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                                    </div>

                                    <div class="col-md-1">
                                        <asp:Label ID="lblmsg" runat="server" CssClass=" btn btn-danger primaryBtn"></asp:Label>
                                    </div>


                                </div>

                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccResCode" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchpIndp" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImgbtnFindProjind" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindProjind_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlProjectInd" runat="server" CssClass="chzn-select form-control inputTxt" style="width:336px;">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-2 pading5px">

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" Style="margin-left:-148px;" >Ok</asp:LinkButton>



                                    </div>
                                </div>



                                 <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblGrp" runat="server" CssClass="lblTxt lblName" Text="Group"></asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                            <asp:ListItem Selected="True">Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem >Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>



                                </div>
                            </div>
                        </fieldset>

                    </div>

                    <asp:GridView ID="gvPrjRpt" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvPrjtrbal_RowDataBound" ShowFooter="True" Width="658px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcActDesc" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                                           %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />

                                    <FooterTemplate>

                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFT" runat="server" Font-Bold="True" Font-Size="12px"
                                                          ForeColor="#000" Style="text-align: right" Text="Total(in Tk.):"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px"
                                                          ForeColor="#000" Style="text-align: right" Text="Net Balance :"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>

                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvqty" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRate" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debit(in Tk.)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAmt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFTDrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                          ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="lgvNbal" runat="server" Font-Bold="True" Font-Size="12px"
                                                          ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>

                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Credit(in Tk.)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCre" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFTCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                          ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style58"></td>
                                            </tr>
                                        </table>

                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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



<%--            <asp:Panel ID="panel11" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Width="90%">
                <table style="width: 100%;">

                    <tr>
                        <td class="style76" width="20px">&nbsp;
                        </td>
                        <td class="style61" width="125px">
                            <asp:Label ID="lblDatefrom" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                Style="text-align: right; color: #FFFFFF;" Text="As on Date:"
                                Width="120px"></asp:Label>
                        </td>
                        <td class="style62" width="80px">
                            <asp:TextBox ID="txtDatefrom" runat="server" Width="80px" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td class="style63" width="22px">&nbsp;</td>
                        <td class="style28">
                            <asp:Label ID="lblmsg" runat="server" BackColor="#FFECFF" BorderColor="#996633"
                                BorderStyle="Solid" BorderWidth="0px" Font-Bold="True" Font-Size="12px"
                                ForeColor="#FF0066"></asp:Label>
                        </td>
                        <td class="style63">&nbsp;
                        </td>
                        <td class="style66">&nbsp;</td>
                        <td class="style67">&nbsp;</td>
                        <td class="style68">&nbsp;</td>
                        <td></td>
                    </tr>

                    <tr>
                        <td>&nbsp;
                        </td>
                        <td class="style87">
                            <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                Style="text-align: right; color: #FFFFFF;" Text="Project Name:" Width="120px"></asp:Label>
                        </td>
                        <td class="style98">
                            <asp:TextBox ID="txtSearchpIndp" runat="server" Style="border-style: solid; border-width: 1px"
                                Width="80px"></asp:TextBox>
                        </td>
                        <td class="style63">
                            <asp:ImageButton ID="ImgbtnFindProjind" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                OnClick="ImgbtnFindProjind_Click" Width="21px" />
                        </td>
                        <td class="style28">
                            <asp:DropDownList ID="ddlProjectInd" runat="server" AutoPostBack="True" Font-Size="12px"
                                Width="400px">
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ddlProjectInd_ListSearchExtender" runat="server" QueryPattern="Contains"
                                TargetControlID="ddlProjectInd">
                            </cc1:ListSearchExtender>
                        </td>
                        <td>&nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="16px"
                                    Height="20px" OnClick="lbtnOk_Click"
                                    Style="text-align: center;" Width="52px"
                                    BackColor="#003366"   ForeColor="#000" BorderColor="White"
                                    BorderStyle="Solid" BorderWidth="1px">Ok</asp:LinkButton>
                        </td>
                        <td>&nbsp;&nbsp;
                        </td>
                        <td>&nbsp;&nbsp;
                        </td>
                        <td>&nbsp;&nbsp;
                        </td>
                        <td>&nbsp;&nbsp;
                        </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="style28"></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="100%">
                <tr>
                    <td colspan="10">
                        
                    </td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

