<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptMRTopSheet.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptMRTopSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function Search_Gridview(strKey, cellNr) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvAccMR.ClientID %>");
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

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            try {

                $("input, select")
                    .bind("keydown",
                        function (event) {
                            var k1 = new KeyPress();
                            k1.textBoxHandler(event);
                        });

                $('.chzn-select').chosen({ search_contains: true });
                $('#<%=this.gvAccMR.ClientID%>').tblScrollable();

            }
            catch (e) {
                alert(e.mesage);
            }
        };

        function FunMoneyReceipt(url) {
            window.open('' + url + '', '_blank');
        }

    </script>


    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
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
            <div class=" card card-fluid" style="min-height: 600px">
                <div class=" card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label for="Label5" runat="server" class=" control-label  lblmargin-top9px ">From</label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputDateBox" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label for="Label6" runat="server" class=" control-label lblmargin-top9px">To</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="inputDateBox"
                                    TabIndex="1"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label for="lblrefno" runat="server" class=" control-label  lblmargin-top9px">Ref.</label>
                                <asp:TextBox ID="txtrefno" runat="server" CssClass="inputTextBox"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HpblnkNew" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_23_CR/MktMoneyReceipt?Type=CustCare">New Money Receipt</asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvAccMR" runat="server" AutoGenerateColumns="False"
                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvAccMR_RowDataBound"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvINSlNo0" runat="server" Font-Bold="True"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MR Date" SortExpression="voudat">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMRDate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>

                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MR No">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchMrNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="MR. No" onkeyup="Search_Gridview(this,2)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMRNo" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque No">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchChqNo" SortExpression="refnum" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Cheque No" onkeyup="Search_Gridview(this,3)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvChqNo" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pay Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayDate" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchproj" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Project Name" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPactdesc" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchunit" BackColor="Transparent" BorderStyle="None" runat="server" Width="140px" placeholder="Unit Name" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvunitdesc" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchcust" BackColor="Transparent" BorderStyle="None" runat="server" Width="160px" placeholder="Customer Name" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcustdesc" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchamt" BackColor="Transparent" BorderStyle="None" runat="server" Width="75px" placeholder="Amount" onkeyup="Search_Gridview(this,8)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMRAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFMRAmt" runat="server" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                    CssClass="btn  btn-primary  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkMoneyRcptPrint" OnClick="lnkMoneyRcptPrint_Click" runat="server" ToolTip="Money Receipt Print" CssClass="btn btn-default btn-xs" Visible="false"><span class="fa fa-print"></span></asp:LinkButton>
                                                <asp:HyperLink ID="hlnkMoneyRcptPrint" runat="server" Target="_blank" ToolTip="Money Receipt Print" CssClass="btn btn-default btn-xs" Visible="false"><span class="fa fa-print"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkMoneyRcptEdit" ToolTip="Edit" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class=" fa fa-edit"></span>
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearusrname" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="User Name" onkeyup="Search_Gridview(this,12)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblogvusrname" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="pactcode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpactcode" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="usircode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusircode" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="usircode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmrdate" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vounum" >
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvounum" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
