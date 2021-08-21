<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ToDaysAppointment.aspx.cs" Inherits="RealERPWEB.F_47_Kpi.ToDaysAppointment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/ScrollableGridPlugin.js"></script>
    <script src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }

    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-6 pading5px ">
                                        <asp:Label ID="lblDatefrm" runat="server" CssClass="lblTxt lblName" Text="From:"></asp:Label>

                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtFrom">
                                        </cc1:CalendarExtender>

                                        <asp:Label ID="lbldateto" runat="server" Text="To:" TabIndex="1" CssClass=" smLbl_to"></asp:Label>

                                        <asp:TextBox ID="txtTo" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="2"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtTo">
                                        </cc1:CalendarExtender>

                                    </div>

                                    <div class="clearfix"></div>


                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblSalesTeam" runat="server" CssClass="lblTxt lblName" Text="Sales Team:"></asp:Label>



                                        <asp:TextBox ID="txtSrchSalesTeam" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgSearchSalesTeam" runat="server" OnClick="imgSearchSalesTeam_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
                                    </div>



                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlSalesTeam" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                            OnSelectedIndexChanged="ddlSalesTeam_SelectedIndexChanged" TabIndex="5">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>


                                    </div>
                                    <div class="clearfix"></div>


                                </div>


                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblClient" runat="server" CssClass="lblTxt lblName" Text="Client:"></asp:Label>



                                        <asp:TextBox ID="txtSrchClient" runat="server" TabIndex="6" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgSearchClient" runat="server" OnClick="imgSearchClient_Click" TabIndex="7" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
                                    </div>



                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlClientList" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                            OnSelectedIndexChanged="ddlClientList_SelectedIndexChanged" TabIndex="8">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                    </div>
                                    <div class="clearfix"></div>


                                </div>

                                <div class="form-group">
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblph" runat="server" CssClass="lblTxt lblName" Text="Phone:"></asp:Label>
                                        <asp:Label ID="lblphone" runat="server" CssClass="lblTxt lblName txtAlgLeft" ></asp:Label>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                            </div>
                        </fieldset>

                        <div class="table-responsive">
                            <asp:GridView ID="gvAppment" runat="server" AllowPaging="True" OnRowDeleting="gvAppment_RowDeleting"
                                AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                CssClass="table table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="10px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:CommandField ShowDeleteButton="True" />


                                    <asp:TemplateField HeaderText="Meeting Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtmtingdate" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"cdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtmtingdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtmtingdate">
                                            </cc1:CalendarExtender>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Panel ID="PnlProject" runat="server">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:TextBox ID="txtProSearch" runat="server" CssClass="inputTxt inputName smltxtBox" TabIndex="10"></asp:TextBox>


                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="imgSearchProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchProject_Click" TabIndex="11"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </div>


                                                    <div class="ddlListPart">
                                                        <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" CssClass="smDropDownGrid inputTxt"
                                                            OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"
                                                            TabIndex="12">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Description">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Panel ID="Panel2" runat="server">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:TextBox ID="txtUnitSearch" runat="server" CssClass="inputTxt inputName smltxtBox" TabIndex="13"></asp:TextBox>


                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="imgSearchUnit" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchUnit_Click" TabIndex="14"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </div>

                                                    <div class="ddlListPart">
                                                        <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="True" CssClass="smDropDownGrid inputTxt"
                                                            OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged"
                                                            TabIndex="15">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>


                                            </asp:Panel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvunitsize" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"usize")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvacrate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"bgdrate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Parking">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvacparking" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"pamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Other">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvacother" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"othamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvactotal" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>






                                    <asp:TemplateField HeaderText="Offered Price">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvofprice" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Parking">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvofpamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"ofpamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Other">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvofother" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"ofothamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvofftotal" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"oftuamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Booking">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvbookamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"bookamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Destination">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtdestination" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"destintion")) %>'
                                                Width="80px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Call/Visit Time">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkTotal_Click">Total :</asp:LinkButton>


                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtcallvistime" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"calovtime")) %>'
                                                Width="60px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Payment Schedule /Feed Back">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="30px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                TextMode="MultiLine" Width="200px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkappupdate" runat="server" OnClick="lnkappupdate_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>

                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Next Scheduled App">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvna" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"napnt")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtgvna_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvna">
                                            </cc1:CalendarExtender>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Remarks" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtremarks" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"remarks")) %>'
                                                Width="70px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
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

