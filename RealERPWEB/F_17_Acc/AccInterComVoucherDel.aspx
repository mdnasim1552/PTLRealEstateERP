<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccInterComVoucherDel.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccInterComVoucherDel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            try {
                $('.chzn-select').chosen({ search_contains: true });
                $('#<%=this.gvAccIntercom.ClientID%>').tblScrollable();
            }
            catch (e) {
                alert(e.mesage);
            }
        };
        function Search_Gridview(strKey, cellNr) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvAccIntercom.ClientID %>");
            var rowData;
            for (var i = 0; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].cells[cellNr].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

    </script>

    <style>
        .grvContentarea tr td:last-child a span {
            margin: 0 5px !important;
        }

        .modal-lg {
            max-width: 70%;
        }
    </style>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
            <div class="card card-fluid" style="min-height: 600px">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <label for="lblfrmdate" runat="server" class="control-label">From Date</label>
                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                        </div>
                        <div class="col-md-2">
                            <label for="lbltodate" runat="server" class=" control-label">To Date</label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1">
                            <label for="lblrefno" runat="server" class="control-label">Voucher Ref</label>
                            <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" Style="margin-top: 27px">Ok</asp:LinkButton>
                        </div>

                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gvAccIntercom" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvAccIntercom_RowDataBound" ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvINSlNo0" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Voucher Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvvoudat" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>

                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Voucher">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchvounum1" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Voucher No" onkeyup="Search_Gridview(this,2)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblvounum" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reference">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchrefnum" SortExpression="refnum" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Reference No" onkeyup="Search_Gridview(this,3)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtINgvteamdesc" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchproj" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Project Name" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpactdesc" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Party Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchpayto" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Party Name" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsirdesc" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Accounts Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtcactdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Accounts Name" onkeyup="Search_Gridview(this,8)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcactdesc" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Resource Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtresdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Resource Name" onkeyup="Search_Gridview(this,8)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvresdesc" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Voucher Amount">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchamt" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Vou. Amount" onkeyup="Search_Gridview(this,9)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvouamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFvouamt" runat="server" Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Narration">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchvenar" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Narration" onkeyup="Search_Gridview(this,6)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvounar" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllCheckid" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllCheckid_CheckedChanged" Width="20px" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkvmrno" runat="server" AutoPostBack="true" OnCheckedChanged="chkvmrno_CheckedChanged" Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv")) == "True" ? false : true %>'
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                                Width="30px" CssClass="btn btn-default btn-xs" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnChekedId" runat="server" OnClientClick="return FunAppConfirm();" OnClick="lnkbtnChekedId_Click" ToolTip="Cheked"><span class=" fa fa-check "></span>
                                            </asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkVoucherPrint" runat="server" Target="_blank" ToolTip="Voucher Print" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                            <asp:HyperLink ID="hlnkVoucherEdit" runat="server" ToolTip="Edit" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-edit"></span></asp:HyperLink>
                                            <asp:LinkButton ID="lbtnDeleteVoucher" runat="server" ToolTip="Delete" OnClick="lbtnDeleteVoucher_Click" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="110px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="90px" VerticalAlign="Middle" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="User Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearusrname" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="User Name" onkeyup="Search_Gridview(this,12)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvusrname" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                Width="70px"></asp:Label>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

