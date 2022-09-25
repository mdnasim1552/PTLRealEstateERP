
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccRpCodeRecon.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccRpCodeRecon" %>

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
            $('.chzn-select').chosen({ search_contains: true });


        };
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
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblChqNo" runat="server"
                                            Text="Cheque No" Font-Size="11px" CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtChqSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                    </div>


                                    <div class="col-md-5  asitCol5 pading5px">
                                        <asp:RadioButtonList ID="rbtnGroup" runat="server" CssClass="rbtnList1"
                                            RepeatColumns="6" RepeatDirection="Horizontal"
                                            TabIndex="2">
                                            <%--<asp:ListItem>Journal(C. Head)</asp:ListItem>--%>
                                            <asp:ListItem>Deposit</asp:ListItem>
                                            <asp:ListItem Selected="True">Payment</asp:ListItem>
                                            <asp:ListItem>Both</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton ID="lbtnGetData0" runat="server"
                                            OnClick="lbtnGetData_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>
                                    </div>

                                </div>

                                <div class="form-group">
                                    

                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True"
                                            Text="Date Range" CssClass="lblTxt lblName" Visible="true"></asp:Label>

                                        <asp:TextBox ID="TxtDate1" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="ClndrExtDate1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="TxtDate1"></cc1:CalendarExtender>



                                        <asp:Label ID="Label7" runat="server" Font-Bold="True"
                                            Text="To" CssClass="smLbl_to" Visible="true"></asp:Label>

                                        <asp:TextBox ID="TxtDate2" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="ClndrExtDate2" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="TxtDate2"></cc1:CalendarExtender>

                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Font-Bold="True"
                                            Text="Size:"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="5">
                                            <asp:ListItem Value="10">10</asp:ListItem>
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
                                    <div class="col-md-3">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblrpname" runat="server"
                                            Text="RP Name" Font-Size="11px" CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtSearchName" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="imgSearchRpName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgSearchRpName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>


                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlRpName" runat="server"
                                            Width="300px" AutoPostBack="True" CssClass="ddlistPull chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-2">
                                      <asp:Label ID="lblsegment" runat="server" CssClass=" smLbl_to">Slap </asp:Label>
                                        <asp:DropDownList ID="ddlRange" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="120px">
                                        <asp:ListItem Value="rang1">Slab = 1-5000</asp:ListItem>
                                        <asp:ListItem Value="rang2">Slab = 5001-50000</asp:ListItem>
                                        <asp:ListItem Value="rang3" >Slab = 50001-100000</asp:ListItem>
                                        <asp:ListItem Value="rang4">Slab = 100001-Above</asp:ListItem>
                                        <asp:ListItem Value="rang5" Selected="True">Slab = ALL </asp:ListItem>                                       
                                            
                                        </asp:DropDownList> 
                                        
                                        

                                         

                                    </div>

                                    <div class="col-md-4">
                             <asp:RadioButtonList ID="rbtnordering" runat="server" CssClass="rbtnList1"
                                            RepeatColumns="6"
                                            RepeatDirection="Horizontal" Style="text-align: left">
                                            <asp:ListItem Value="Issue">Issue No</asp:ListItem>
                                            <asp:ListItem Value="Hourneddate">Hourned Date</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="rpname">RP group</asp:ListItem>                                          
                                        </asp:RadioButtonList>


                                    </div>
                                        <%--<asp:CheckBox ID="AssenCheque" runat="server" CssClass="chkBoxControl margin5px" Text="Assending (Issue Number)" />--%>


                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                    <asp:GridView ID="gv1" runat="server"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gv1_PageIndexChanging"
                                    ShowFooter="True" Width="104px" PageSize="20" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowCancelingEdit="gv1_RowCancelingEdit" OnRowEditing="gv1_RowEditing"
                                    OnRowUpdating="gv1_RowUpdating1" OnRowDataBound="gv1_RowDataBound">
                                    <RowStyle />


                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server"
                                                    Style="text-align: right; font-size: 11px;"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="actcode" Visible="false">
                                            <EditItemTemplate>
                                                <asp:Label ID="lbgractcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' Width="20px"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label701" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                            <EditItemTemplate>
                                                <asp:Label ID="lbgrrescode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="20px"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSUBCODE" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="cactcode" Visible="false">
                                            <EditItemTemplate>
                                                <asp:Label ID="lbgrcactcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>' Width="20px"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VOUNUM" Visible="False">
                                            <EditItemTemplate>
                                                <asp:Label ID="lbgrvounum" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>' Width="20px"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVOUNUM" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                    Width="87px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issue Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcldate" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Vou.Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVOUDAT" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Recon. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecDAT" runat="server" Font-Size="11px"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")%>'
                                                    Width="66px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                        <asp:TemplateField HeaderText="Rp Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRpName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlRpName" CssClass="chzn-select" runat="server" Width="200px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ddlRpName_ListSearchExtender" runat="server"
                                                    Enabled="True" QueryPattern="Contains" TargetControlID="ddlRpName">
                                                </cc1:ListSearchExtender>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Issue #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvissueno" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Voucher #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVOUNUM1" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                      ForeColor="#000" Style="text-align: right">Grand Total:</asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Cheq./ Ref. No.">

                                            <ItemTemplate>
                                                <asp:Label ID="lblREFNO" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Caash/Bank">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcashorbank" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ctransdes")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Accounts Head">

                                            <ItemTemplate>
                                                <asp:Label ID="lblTRANSDES" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details Head">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDetailsHead" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc1")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Deposit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDeposit" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDpAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                      ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayment" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPayAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                      ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Narration">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Narration"
                                                                Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="style60">
                                                            <asp:HyperLink ID="hlbtnbtbCdataExel" runat="server" CssClass="btn btn-warning primaryBtn" >Export Exel</asp:HyperLink>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvVarnar" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rpcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRpCode" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>

                                    <%--  <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <PagerStyle BackColor="#FFE0C0" Font-Bold="True" Font-Names="Verdana" 
                                    Font-Size="16px" ForeColor="Black" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True"   ForeColor="#000" />
                                <HeaderStyle BackColor="#993300" Font-Bold="True" ForeColor="#E7E7FF" />
                                <AlternatingRowStyle BackColor="#CCFFFF" />--%>

                                   <FooterStyle CssClass="grvFooter"/>
<EditRowStyle />
<AlternatingRowStyle />
<PagerStyle CssClass="gvPagination" />
<HeaderStyle CssClass="grvHeader" />


                                </asp:GridView>
</div>
                    </div></div>









        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

