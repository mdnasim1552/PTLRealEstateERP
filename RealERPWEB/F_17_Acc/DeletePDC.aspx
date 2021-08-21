<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="DeletePDC.aspx.cs" Inherits="RealERPWEB.F_17_Acc.DeletePDC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gv1 = $('#<%=this.dgv1.ClientID %>');


            gv1.Scrollable();


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
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

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblBankCode" CssClass="lblTxt lblName" runat="server">Bank Des.</asp:Label>

                                        <asp:TextBox ID="txtserchBankName" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSrchBank" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSrchBank_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlBankName" runat="server" Font-Bold="True" CssClass=" form-control inputTxt">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblChequeNO" CssClass="lblTxt lblName" runat="server">Cheque No.</asp:Label>

                                        <asp:TextBox ID="txtserchChequeno" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSearchCheqNO" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSearchCheqNO_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-4  pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to"
                                            Text="From"></asp:Label>

                                        <asp:TextBox ID="txtfrmdate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                                        <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to"
                                            Text="To"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>



                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lnkOk_Click">Ok</asp:LinkButton>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:Panel ID="pnlGridPage" runat="server" Visible="false">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgBtnFirst" runat="server" Height="18px"
                                                            ImageUrl="~/Image/First.png" OnClick="imgBtnFirst_Click"
                                                            ToolTip="First Page" TabIndex="9" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgBtnNext" runat="server" Height="18px"
                                                            ImageUrl="~/Image/Next.png" OnClick="imgBtnNext_Click" ToolTip="Next"
                                                            TabIndex="10" />
                                                    </td>
                                                    <td class="style95">
                                                        <asp:Label ID="lblCurPage" runat="server" BackColor="White" ForeColor="Black"
                                                            Height="18px" Style="text-align: center" Width="30px" TabIndex="11">1</asp:Label>
                                                    </td>
                                                    <td class="style91">
                                                        <asp:ImageButton ID="imgBtnPerv" runat="server" Height="18px"
                                                            ImageUrl="~/Image/Prev.png" OnClick="imgBtnPerv_Click" ToolTip="Previous"
                                                            TabIndex="12" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgBtnLast" runat="server" Height="18px"
                                                            ImageUrl="~/Image/Last.png" OnClick="imgBtnLast_Click" ToolTip="Last Page"
                                                            TabIndex="13" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                    <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnRowDataBound="dgv1_RowDataBound" ShowFooter="True"
                        PageSize="100">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")) %>' Width="10px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvAccCod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                        Width="49px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cat.Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgcatCod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                        Width="49px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ResCode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lgcUcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Name">
                                <HeaderTemplate>
                                    <table style="width: 180px;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Bank Name"
                                                    Width="100px"></asp:Label>
                                            </td>

                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvbankname" runat="server" Font-Bold="true"
                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "cactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")).Trim(): "")  %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Voucher #" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPVnum" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue #">
                                <ItemTemplate>
                                    <asp:Label ID="lgvvounum1" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPVDate" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acc. Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgactdesc" runat="server"
                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>'
                                        Width="190px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <FooterStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Ref">
                                <ItemTemplate>
                                    <asp:Label ID="lgvisunum" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cheque No.">
                                <ItemTemplate>
                                    <asp:Label ID="lgvchnono" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cheque Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvchdat" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcramt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dr. Amt" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lgvdramt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvParName" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkvmrno" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                        Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbok" runat="server" CommandArgument="lbok"
                                        OnClick="lbok_Click" Width="30px">Delete</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvBill" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
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
            </div>






            <table style="width: 100%;">
                <%--<tr>
                    <td colspan="12">

                        <asp:Panel ID="P1" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                            BorderWidth="1px">
                            <table style="width: 99%; height: 2px;">
                                <tr>
                                    <td class="style223" colspan="12">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style235">
                                                        <asp:Label ID="lblBankCode" runat="server" CssClass="label2" Text="Bank Des.:"
                                                            Width="68px"></asp:Label>
                                                    </td>
                                                    <td class="style232">
                                                        <asp:TextBox ID="txtserchBankName" runat="server" BorderStyle="None"
                                                            Width="97px"></asp:TextBox>
                                                    </td>
                                                    <td class="style233">
                                                        <asp:ImageButton ID="imgbtnSrchBank" runat="server" Height="16px"
                                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnSrchBank_Click" TabIndex="1"
                                                            Width="16px" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlBankName" runat="server" Font-Bold="True"
                                                            Font-Size="12px" TabIndex="2" Width="400px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style228">
                                        <asp:Label ID="lblChequeNO" runat="server" CssClass="label2" Height="16px"
                                            Text="Cheque No:" Width="70px"></asp:Label>
                                    </td>
                                    <td class="style205">
                                        <asp:TextBox ID="txtserchChequeno" runat="server" BorderStyle="None"
                                            TabIndex="4" Width="97px"></asp:TextBox>
                                    </td>
                                    <td class="style234">
                                        <asp:ImageButton ID="imgbtnSearchCheqNO" runat="server" Height="16px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnSearchCheqNO_Click"
                                            TabIndex="5" Width="16px" />
                                    </td>
                                    <td class="style207">&nbsp;</td>
                                    <td class="style208">&nbsp;</td>
                                    <td class="style227">&nbsp;</td>
                                    <td class="style224"></td>
                                    <td class="style224">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style228">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="label2" Text="From :"
                                            Width="50px"></asp:Label>
                                    </td>
                                    <td class="style205">
                                        <asp:TextBox ID="txtfrmdate" runat="server" AutoPostBack="True"
                                            BorderStyle="None" BorderWidth="1px" Width="97px" TabIndex="6"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                    </td>
                                    <td class="style234">
                                        <asp:Label ID="lbltodate" runat="server" CssClass="label2" Height="16px"
                                            Text="To:"></asp:Label>
                                    </td>
                                    <td class="style207">
                                        <asp:TextBox ID="txttodate" runat="server" AutoPostBack="True"
                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Width="97px"
                                            TabIndex="7"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </td>
                                    <td class="style208">
                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="button" Font-Bold="True"
                                            OnClick="lnkOk_Click" Width="78px" TabIndex="8">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style227"></td>
                                    <td class="style224">&nbsp;</td>
                                    <td class="style224">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="12"></td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

