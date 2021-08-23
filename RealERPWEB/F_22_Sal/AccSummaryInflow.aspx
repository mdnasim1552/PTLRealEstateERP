<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccSummaryInflow.aspx.cs" Inherits="RealERPWEB.F_22_Sal.AccSummaryInflow" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .table-bordered th {
            font-size: 15px;
            font-family: "Century Gothic";
        }

        .table-bordered tr td {
            padding: 6px 5px;
        }

        .table-bordered td {
            font-size: 13px;
            font-family: "Century Gothic";
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {



            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });


            $('.chzn-select').chosen({ search_contains: true });

        }
    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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

            <div class=" card card-fluid">
                <div class=" card-body" style="min-height: 250px;">
                    <br />
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="FromDate" id="lblDaterange" runat="server">Company</label>

                            </div>
                        </div>
                        <div class="col-md-2 pading5px ">
                            <asp:DropDownList ID="ddlcomplist" runat="server" OnSelectedIndexChanged="ddlcomplist_SelectedIndexChanged" AutoPostBack="true" CssClass="custom-select  chzn-select" TabIndex="3">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="FromDate" id="Label1" runat="server">Project</label>

                            </div>
                        </div>
                        <div class="col-md-3 pading5px ">
                            <asp:DropDownList ID="ddlProjectName" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" runat="server"
                                CssClass="custom-select  chzn-select" TabIndex="3">
                            </asp:DropDownList>


                        </div>





                        <div class="col-md-1 d-none">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="FromDate" id="Label2" runat="server">Customer</label>

                            </div>
                        </div>

                        <div class="col-md-3 pading5px d-none">
                            <asp:DropDownList ID="ddlcustomerlist" runat="server" Width="280px" CssClass="custom-select  chzn-select" TabIndex="3">
                            </asp:DropDownList>

                        </div>

                        <div class="col-md-1 d-none">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="FromDate" id="Label3" runat="server">Date</label>
                            </div>
                        </div>
                        <div class="col-md-2 d-none">
                            <asp:TextBox ID="txtDatefrom" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                TargetControlID="txtDatefrom" Enabled="true"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1 ">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Show </asp:LinkButton>
                        </div>

                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered"
                                ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Clients Name" ControlStyle-Width="300px">
                                        <ItemTemplate>

                                            <a target="_blank" href="../F_22_Sal/AccSumCustPayStatus.aspx?Comcod=<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>&CustID=<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>&Pid=<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>">
                                                <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>                                             
                                            </a>

                                        </ItemTemplate>
                                        <ItemStyle Width="300px" />
                                    </asp:TemplateField>






                                    <asp:TemplateField HeaderText="Flat No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: left" Width="30px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitinf")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Flat <br> Size.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: right" Width="40px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unitsize")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvtpasuamt" runat="server" ForeColor="Black">Total</asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Assumed Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: right" Width="85px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvtpauamt" runat="server" ForeColor="Black"
                                                Width="75px"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: right" Width="85px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvtpaidamt" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Receivable">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: right" Width="85px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvbale")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvtrecvbale" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-ForeColor="brown" HeaderStyle-BackColor="brown" FooterStyle-BackColor="brown" ItemStyle-BackColor="brown" HeaderStyle-CssClass="p-0" ItemStyle-CssClass="p-0" FooterStyle-CssClass="p-0">
                                        <ItemTemplate>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acc Assumed Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: right" Width="85px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accuamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvttaaccuamt" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acc Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: right" Width="85px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accpaidamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvttaccpaidamt" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acc Receivable">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: right" Width="85px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accrecvbale")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvttaccrecvbale" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-ForeColor="brown" HeaderStyle-BackColor="green" FooterStyle-BackColor="green" ItemStyle-BackColor="green" HeaderStyle-CssClass="p-0" ItemStyle-CssClass="p-0" FooterStyle-CssClass="p-0">
                                        <ItemTemplate>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sold Apt. Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: right" Width="85px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlsolamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvttlsolamt" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sold Apt. Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: left" Width="85px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttsolrecv")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvttsolrecv" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sold Apt. Due">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: right" Width="85px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttldue")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvttldue" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle CssClass="grvFooter text-right" Font-Size="13px" Font-Bold="false" />
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

