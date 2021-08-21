<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProTarget.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.ProTarget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(PageLoad);

        });

        function PageLoad() {
            var gv = $('#<%=this.gvProTarget.ClientID %>');
            gv.Scrollable();
        }
    </script>
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
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProjectList" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-3 pading5px  asitCol3">
                                        <asp:DropDownList ID="ddlProject" runat="server" TabIndex="2" CssClass="chzn-select form-control inputTxt" style="width:336px;">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectDesc" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>


                                    </div>
                                    <div class="col-md-1 pading5px">

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" Style="margin-left:139px;">Ok</asp:LinkButton>

                                        </div>
                                    </div>

                                    <div class="col-md-3 pading5px">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" CssClass=" lblProgressBar" Text="Please wait . . . . . . ."></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>


                            </div>
                        </fieldset>
                    </div>
                    <asp:Panel ID="PnlColoumn" runat="server" Visible="False">


                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lbl01" runat="server" CssClass="lblTxt lblName">Start Date</asp:Label>

                                        <asp:Label ID="lblStartDate" runat="server" CssClass=" smLbl_to" ></asp:Label>
                                        <asp:Label ID="lbl2" runat="server" CssClass="lblTxt lblName">End Date:</asp:Label>

                                        <asp:Label ID="lblEndDate" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <asp:Label ID="lbl3" runat="server" CssClass="lblTxt lblName" >Duration</asp:Label>
                                        <asp:Label ID="lblDuration" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                            CssClass="ddlPage">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>

                                        </asp:DropDownList>
                                        <div class="col-md-4">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblColGroup" runat="server" BackColor="#660033" Font-Size="20px"
                                                            ForeColor="green" Style="text-align: center;" Width="26px" Height="20px">1</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP1" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="green" Height="16px" OnClick="lbtngvP_Click"
                                                            Style="text-align: center" Width="17px">1</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP2" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="green" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">2</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP3" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="green" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">3</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP4" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="green" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">4</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP5" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="green" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">5</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP6" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="green" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">6</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP7" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="White" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">7</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP8" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="green" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">8</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP9" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="green" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">9</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                    </div>
                                </div>
                            </fieldset>


                            
                        </asp:Panel>
                        <asp:GridView ID="gvProTarget" runat="server" AutoGenerateColumns="False"
                            HeaderStyle-CssClass="HeaderStyle" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="16px" ShowFooter="True"
                            OnPageIndexChanging="gvProTarget_PageIndexChanging" AllowPaging="True">
                            <PagerStyle />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item Description ">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="red" OnClick="lbtnUpdate_Click"></asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemDesc" runat="server" Font-Size="12px"
                                            Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "flrdes").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")).Trim(): "") 
                                                                         
                                                                    %>'
                                            Width="180px">   </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal0" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="red" OnClick="lbtnTotal_Click"></asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted Qty">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFBgdqty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbgdqty" runat="server" CssClass="style101"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Allocation Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttrqty" runat="server" CssClass="style101"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFAloqty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Difference">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdiffqty" runat="server" CssClass="style101"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "difqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFDifqty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"> Total :</asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="YM1">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty001" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym1qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM2">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty002" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym2qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM3">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty003" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym3qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM4">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty004" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym4qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM5">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty005" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym5qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM6">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty006" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym6")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym6qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM7">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty007" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym7qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM8">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty008" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym8qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM9">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty009" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym9")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym9qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM10" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty010" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym10qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM11" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty011" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym11")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym11qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM12" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty012" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym12")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym12qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM13" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty013" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym13")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym13qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM14" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty014" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym14")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym14qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM15" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty015" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym15")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym15qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM16" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty016" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym16")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym16qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM17" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty017" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym17")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym17qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM18" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty018" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym18")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym18qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM19" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty019" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym19")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym19qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM20" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty020" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym20")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym20qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM21" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty021" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym21")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym21qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM22" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty022" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym22")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym22qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM23" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty023" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym23")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym23qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM24" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty024" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym24")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym24qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM25" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty025" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym25")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym25qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM26" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty026" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym26")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym26qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM27" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty027" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym27")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym27qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM28" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty028" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym28")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym28qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM29" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty029" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym29")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym29qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM30" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty030" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym30")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym30qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM31" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty031" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym31")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym31qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM32" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty032" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym32")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym32qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM33" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty033" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym33")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym33qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM34" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty034" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym34")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym34qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM35" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty035" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym35")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym35qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM36" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty036" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym36")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym36qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM37" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty037" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym37")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym37qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM38" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty038" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym38")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym38qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM39" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty039" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym39")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym39qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM40" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty040" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym40")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym40qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM41" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty041" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym41")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym41qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM42" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty042" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym42")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym42qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM43" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty043" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym43")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym43qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM44" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty044" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym44")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym44qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM45" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty045" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym45")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym45qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ym46" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty046" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym46")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym46qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM47" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty047" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym47")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym47qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM48" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty048" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym48")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym48qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM49" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty049" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym49")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym49qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM50" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty050" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym50")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym50qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM51" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty051" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym51")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym51qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM52" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty052" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym52")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym52qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM53" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty053" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym53")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym53qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM54" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty054" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym54")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym54qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM55" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty055" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym55")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym55qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM56" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty056" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym56")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym56qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM57" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty057" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym57")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym57qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM58" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty058" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym58")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym58qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM59" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty059" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym59")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym59qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM60" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvQty060" runat="server" CssClass="style101" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym60")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFym60qty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="black" Style="text-align: right; width: 55px;"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
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

