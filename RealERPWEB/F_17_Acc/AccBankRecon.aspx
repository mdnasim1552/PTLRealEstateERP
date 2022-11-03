<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccBankRecon.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccBankRecon" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });



            var gv = $('#<%=this.gv1.ClientID %>');
            $.keynavigation(gv);
            gv.Scrollable();

        }
    </script>

    <style type="text/css">
        .ajax__calendar .ajax__calendar_container {
            background-color: #ffffff;
            border: 1px solid #646464;
            color: #000000;
            margin-left: 90px;
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label1" runat="server"
                                            Text="Bank Name" Font-Size="11px" CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtBankSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindBankName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindBankName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>


                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="DDListBank" runat="server"
                                            Width="300px" AutoPostBack="True" CssClass="ddlistPull">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnGetData" runat="server"
                                            OnClick="lbtnGetData_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblChqNo" runat="server" CssClass="lblTxt lblName"
                                            Text="Cheque No"></asp:Label>

                                        <asp:TextBox ID="txtChqSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True"
                                            Text="Date Range" CssClass="smLbl_to" Visible="true"></asp:Label>

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

                                       

                                        <asp:CheckBox ID="chkvoudate" runat="server" TabIndex="10" Text="Voucher Date" CssClass="btn btn-primary checkBox" />
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </fieldset>
                        <asp:Button ID="btnExcel" runat="server" class=" btn btn-primary primaryBtn pull-right" Visible="False" OnClick="btnExcel_OnClick" Text="Write To Excel File" /><br />
                        <br />
                        &nbsp;&nbsp;
                        <asp:HyperLink ID="hlnkDownload" runat="server" Target="_blank" CssClass="pull-right "></asp:HyperLink>

                        <asp:GridView ID="gv1" runat="server" AllowPaging="True"
                            CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gv1_PageIndexChanging" OnRowDataBound="gv1_RowDataBound"
                            ShowFooter="True" Width="104px" PageSize="20">
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



                                <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSUBCODE" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VOUNUM" Visible="False">
                                    <ItemTemplate>


                                        <asp:HyperLink ID="lblVOUNUM" runat="server" Font-Size="12px"
                                            CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                            Width="75px"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recon.Date (dd.mm.yyyy)">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <%--    <asp:Label ID="lblRECNDT" runat="server" Font-Size="11px" 
                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")) %>' 
                                                Width="80px"></asp:Label>--%>


                                        <asp:TextBox ID="txtgvRECNDT" runat="server" Font-Size="11px" 
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd.MM.yyyy")) %>'
                                            Width="85px" BackColor="Transparent" BorderStyle="None" ></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtgvRECNDT_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtgvRECNDT"></cc1:CalendarExtender>

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheq./ Ref. No.">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblREFNO" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>


                                        <asp:Label ID="lblVOUDAT" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                            Width="66px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque.Date">
                                    <ItemTemplate>


                                        <asp:Label ID="lblgvchequedat" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy") %>'
                                            Width="66px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vou.No.">
                                    <ItemTemplate>


                                        <asp:HyperLink ID="lblVOUNUM1" runat="server" Font-Size="12px"
                                            CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="75px"></asp:HyperLink>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPayment" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Payment">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDeposit" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <%--<asp:TemplateField HeaderText="Accounts Head">

                                <ItemTemplate>
                                    <asp:Label ID="lblTRANSDES" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="160px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Accounts Head">


                                    <%-- <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblheader" runat="server" Text="Accounts Head"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExelsp" runat="server" CssClass=" btn btn-success btn-xs  fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>--%>


                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectName" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Details Head">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDetailsHead" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc1")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Narration">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvVarnar" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                            Width="220px"></asp:Label>
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



                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />


                        </asp:GridView>

                    </div>



                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

