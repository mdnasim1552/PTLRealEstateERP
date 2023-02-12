<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccDepJournal.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccDepJournal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">



    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {


            var gridview = $('#<%=this.dgv2.ClientID %>');
            $.keynavigation(gridview);
        };
    </script>
    <style>
        .mt20 {
            margin-top: 20px;
        }

        .mt22 {
            margin-top: 22px;
        }
    </style>

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
            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row mb-2">
                        <div class="col-md-1.5  mt22">
                            <asp:Label ID="lblcurVounum" runat="server" CssClass=" lblName lblTxt" Text="Current Voucher No." Width="120"></asp:Label>
                        </div>
                        <div class="col-md-.5 mt20  ">
                            <asp:TextBox ID="txtcurrentvou" runat="server" AutoCompleteType="Disabled" AutoPostBack="true" CssClass="form-control form-control-sm" Width="65"></asp:TextBox>
                        </div>
                        <div class="col-md-1 mt20  ">
                            <asp:TextBox ID="txtCurrntlast6" runat="server" AutoPostBack="True" ReadOnly="True" ToolTip="You Can Change Voucher Number." Width="65" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>
                        <div class="col-md-2  ">
                            <asp:Label ID="lbltxtDate" runat="server" CssClass=" lblName lblTxt" Text="Voucher Date"></asp:Label>
                            <asp:TextBox ID="lbldate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="lbldate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd.MM.yyyy" TargetControlID="lbldate"></cc1:CalendarExtender>
                            <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn primaryBtn disabled" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row" style="min-height:500px;">
                        <div class="table table-responsive">
                            <asp:Panel ID="pnlBill" runat="server">
                                <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" CssClass=" table-striped  table-bordered grvContentarea" Width="672px" ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpclCod" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/C Description" ItemStyle-Font-Size="9px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccdesc1" runat="server" CssClass="GridLebelL"
                                                    Font-Names="Verdana" Font-Size="11px"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                                    Width="350px"></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="11px" />

                                            <FooterTemplate>
                                                <asp:Label ID="lbltotal" runat="server" CssClass=" lblName lblTxt" Font-Bold="True" Font-Size="12px" Text="Total"></asp:Label>
                                                <asp:LinkButton ID="btnTotal" runat="server" Font-Bold="True" Visible="false"
                                                    OnClick="lbtnTotal_Click" CssClass="btn btn-sm btn-primary primarygrdBtn pull-right">Total :</asp:LinkButton>
                                            </FooterTemplate>

                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvDrAmt" runat="server"  ReadOnly="true"
                                                     Font-Size="12px" Style="text-align: right;"
                                                    Width="80px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" ReadOnly="True" ForeColor="Black"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvCrAmt" runat="server" 
                                                     ReadOnly="true" Style="text-align: right;"
                                                    Width="80px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" Height="22px" ForeColor="Black"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>





                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPaginationNew" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>

                                <div class="form-horizontal" style="margin-top: 10px;">
                                    <div class="form-group">
                                        <div class="col-md-12 pading5px">
                                            <asp:Label ID="lblRefNum" runat="server" CssClass=" lblName lblTxt" Width="130" Text="Ref./Cheq No/Slip No."></asp:Label>
                                            <asp:TextBox ID="txtRefNum" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" Width="200"></asp:TextBox>

                                            <asp:Label ID="lblSrInfo" runat="server" CssClass=" lblName lblTxt" Text="Other ref.(if any)"></asp:Label>
                                            <asp:TextBox ID="txtSrinfo" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" Width="200"></asp:TextBox>
                                            <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass=" btn btn-danger btn-sm primaryBtn" Visible="false" OnClick="lnkFinalUpdate_Click" Width="100px">Final Update</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12 pading5px">
                                            <asp:Label ID="lblNaration0" runat="server" CssClass=" lblName lblTxt" Text="Narration" Width="130"></asp:Label>
                                            <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled" TextMode="MultiLine" Rows="2" Width="505px" CssClass=" form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">
                                </asp:Panel>
                            </div>
                        </fieldset>
                        
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--<table style="width: 100%; height: 20px; margin-bottom: 0px;">
                    <tr>
                        <td class="style200">
                            <asp:Label ID="lblcurVounum" runat="server" CssClass="label2"
                                Text="Current Voucher No." Width="120px"></asp:Label>
                        </td>
                        <td class="style204">
                            <asp:TextBox ID="txtcurrentvou" runat="server" AutoPostBack="True"
                                CssClass="ddl" ReadOnly="True" Width="40px"></asp:TextBox>
                        </td>


                        <td class="style203" align="left">
                            <asp:TextBox ID="txtCurrntlast6" runat="server" AutoPostBack="True"
                                CssClass="ddl" ReadOnly="True" ToolTip="You Can Change Voucher Number."
                                Width="40px"></asp:TextBox>
                        </td>


                        <td class="style210">
                            <asp:Label ID="lbltxtDate" runat="server" CssClass="label2" Text="Voucher Date"
                                Width="80px"></asp:Label>
                        </td>
                        <td class="style207">
                            <asp:Label ID="lbldate" runat="server" BorderStyle="Solid" Width="100px"
                                BackColor="White" BorderWidth="1px"></asp:Label>

                            </cc1:CalendarExtender>
                        </td>
                        <td class="style202">&nbsp;</td>
                        <td>
                            <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True"
                                Font-Size="12px" ForeColor="White"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>--%>

    <%--<tr>
            <td class="style199">&nbsp;<asp:Label ID="lblRefNum" runat="server" CssClass="label2"
                Text="Ref./Cheq No/Slip No." Width="120px"></asp:Label>
            </td>
            <td class="style190">&nbsp;<asp:TextBox ID="txtRefNum" runat="server" 
                CssClass="ddl" Width="166px"></asp:TextBox>
            </td>
            <td class="style191">&nbsp;<asp:Label ID="lblSrInfo" runat="server" CssClass="label2"
                Text="Other ref.(if any)" Width="120px"></asp:Label>
            </td>
            <td class="style209">&nbsp;<asp:TextBox ID="txtSrinfo" runat="server" AutoCompleteType="Disabled"
                CssClass="ddl" Width="265px"></asp:TextBox>
            </td>
            <td>&nbsp;
            </td>
            <td class="style178">&nbsp;</td>
            <td class="style192">&nbsp;</td>
            <td class="style193">&nbsp;</td>
        </tr>--%>

    <%--<tr>
            <td class="style199" style="text-align: right; vertical-align: top">
                <asp:Label ID="lblNaration0" runat="server" CssClass="label2" Text="Narration"
                    Width="120px"></asp:Label>
            </td>
            <td class="style190" colspan="3">
                <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled"
                    CssClass="ddl" Height="42px" TextMode="MultiLine" Width="596px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td class="style178">&nbsp;</td>
            <td class="style192">&nbsp;</td>
            <td class="style193"></td>
        </tr>--%>
</asp:Content>


