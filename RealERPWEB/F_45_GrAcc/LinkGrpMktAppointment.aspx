
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpMktAppointment.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpMktAppointment" %>

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
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From:"></asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" Text="To:" TabIndex="1" CssClass=" smLbl_to"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox" TabIndex="2"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to " Text="Page Size:" Visible="False"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Visible="False" CssClass="ddlPage62 inputTxt">
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
                                    <div class="col-md-1 pading5px">
                                    </div>
                                    <div class="col-md-1 pading5px">
                                    </div>


                                    <div class="clearfix"></div>


                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblSalesTeam" runat="server" CssClass="lblTxt lblName" Text="Sales Team:"></asp:Label>


                                        <asp:TextBox ID="txtSrcteam" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnteam" runat="server" OnClick="imgbtnteam_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
                                    </div>


                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlSalesTeam" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                            TabIndex="5">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlSalesTeam_ListSearchExtender1" runat="server" QueryPattern="Contains"
                                            TargetControlID="ddlSalesTeam">
                                        </cc1:ListSearchExtender>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbntOk" runat="server" Text="Ok" OnClick="lbntOk_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>

                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </fieldset>

                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="todaysApp" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvtodayapp" runat="server" AllowPaging="True" OnPageIndexChanging="gvtodayapp_PageIndexChanging"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                        CssClass="table table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNotd" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Meeting Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMeetingdattd" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Client Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgClNametd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPhonetd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpronametd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvunitnametd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvunitsizetd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacpricetd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacparkingtd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacothertd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvactoamttd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offered Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvofratetd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvofparkingtd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofpamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgofothertd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofothamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoftoamttd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oftuamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Destination">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcallovipurposetd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "destintion")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discussion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDiscussiontd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Appointment">
                                                <ItemTemplate>
                                                    <asp:Label ID="nappdattd" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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
                            </asp:View>

                            <asp:View ID="Discussionhis" runat="server">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblCleint" runat="server" CssClass="lblTxt lblName" Text="Client:"></asp:Label>

                                        <asp:TextBox ID="txtSrchclient" runat="server" TabIndex="4" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnclient" runat="server" OnClick="imgbtnclient_Click" TabIndex="5" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlClientList" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                            TabIndex="6">
                                        </asp:DropDownList>


                                    </div>
                                    <div class="col-md-1 pading5px">
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="table-responsive">
                                    <asp:GridView ID="gvclient" runat="server" AllowPaging="True" OnPageIndexChanging="gvclient_PageIndexChanging"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                        CssClass="table table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Phone No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPhonech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Meeting Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMeetingdat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpronamech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvunitname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvunitsizech" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacpricech" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacparkingch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacotherch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvactoamtch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offered Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvofratech" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvofparkingch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofpamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgofotherch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofothamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoftoamtch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oftuamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Diff. In %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdiffinper" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dinper")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Destination">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcallovipurposech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "destintion")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Call/Visit time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcallovtimech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "calovtime")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discussion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDiscussion0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Appointment">
                                                <ItemTemplate>
                                                    <asp:Label ID="nappdat0" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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
                            </asp:View>

                            <asp:View ID="OffPer" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvoffper" runat="server" AllowPaging="True" OnPageIndexChanging="gvoffper_PageIndexChanging"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                        CssClass="table table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Meeting Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMeetingdat0" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Client Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgClName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address & Phone No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPhone1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpronameop" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvunitnameop" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacpriceop" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacparkingop" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacotherop" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvactoamtop" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offered Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvofrateop" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvofparkingop" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofpamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgofotherop" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofothamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoftoamtop" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oftuamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Diff in %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdiffinperoff" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dinper")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Destination">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcallovipurposeop" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "destintion")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Call/Visit time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcallovtimeop" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "calovtime")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discussion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDiscussion1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Appointment">
                                                <ItemTemplate>
                                                    <asp:Label ID="nappdat1" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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
                            </asp:View>

                            <asp:View ID="ViewSalvaue" runat="server">

                                <div class="table-responsive">
                                    <asp:GridView ID="gvSal" runat="server" AllowPaging="True" OnPageIndexChanging="gvSal_PageIndexChanging"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvSal_RowDataBound"
                                        CssClass="table table-striped table-hover table-bordered grvContentarea" Width="500px">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "saldate")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtxtToamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Width="80px" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvprojectname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnitdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSize" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#, ##0.00; (#, ##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#, ##0.00; (#, ##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFToamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAmount" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#, ##0; (#, ##0); ") %>'
                                                        Width="70px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                        </Columns>

                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>

                            <asp:View ID="LetterCreation" runat="server">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblletterinfo" runat="server" Visible="False" CssClass="lblTxt lblName" Text="Letter Information:"></asp:Label>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <div class="msgHandSt">
                                            <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="table-responsive">
                                    <asp:GridView ID="gvLetter" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                        CssClass="table table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvletcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letcode")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkletter" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>' />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Letter Information">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvteamdesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblClientInfo" runat="server" Visible="False" CssClass="lblTxt lblName" Text="Client Information:"></asp:Label>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                    </div>
                                    <div class="col-md-1 pading5px">
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="table-responsive">
                                    <asp:GridView ID="gvClientLetter" runat="server" AllowPaging="True" OnPageIndexChanging="gvClientLetter_PageIndexChanging"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                        CssClass="table table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllfrm_CheckedChanged"
                                                        Text="ALL " Width="50px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkletter" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>' />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvletcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Client Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvclinetname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvclientAddress" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paddress")) %>'
                                                        Width="350px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvclientMob" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvclientemail" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>

                                <fieldset class="scheduler-border fieldset_D">
                                    <div class="form-horizontal">

                                        <asp:Panel ID="PanelSendMail" runat="server" Visible="False">

                                            <div class="form-group">

                                                <div class="col-md-6 pading5px">
                                                    <div class="input-group">
                                                        <span class="input-group-addon glypingraddon">
                                                            <asp:Label ID="lblForm" runat="server" CssClass="lblTxt" Text="From:  "></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtfromEmail" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-1 pading5px">
                                                    <asp:LinkButton ID="lbtnSend" runat="server" Text="Send" OnClick="lbtnSend_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>

                                                </div>
                                                <div class="col-md-1 pading5px">
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>

                                            <div class="form-group">

                                                <div class="col-md-6 pading5px">
                                                    <div class="input-group">
                                                        <span class="input-group-addon glypingraddon">
                                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtDescription" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-1 pading5px">
                                                </div>

                                                <div class="col-md-1 pading5px">
                                                </div>
                                                <div class="col-md-1 pading5px">
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>

                                        </asp:Panel>

                                    </div>
                                </fieldset>

                            </asp:View>

                            <asp:View ID="ViewProsClient" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvProsClient" runat="server" AllowPaging="True" OnPageIndexChanging="gvProsClient_PageIndexChanging"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                        CssClass="table table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvslno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Client Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvClientName" runat="server" Font-Bold="True" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>' Width="160px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nick Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvClientName" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nickname")) %>'
                                                        Width="120px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPerAddress" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                        Width="160px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Home Phone">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvconNo" runat="server" Width="80px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "homephone")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMobNo" runat="server" Width="80px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Phone">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoffPhone" runat="server" Width="80px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offphone")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvemail" runat="server" Width="140px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>

                            <asp:View ID="ViewAllOfficer" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvalloffper" runat="server" AllowPaging="True" OnPageIndexChanging="gvalloffper_PageIndexChanging"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                        CssClass="table table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Client Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgClNameall" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "prosdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                        Width="150px">
                                                 
                                                 
                                                 
                                                 
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Meeting Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMeetingdatall" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address &amp; Phone No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPhoneall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpronameopall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvunitnameopall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacpriceopall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacparkingopall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacotheropall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvactoamtopall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offered Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvofrateopall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvofparkingopall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofpamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgofotheropall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofothamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoftoamtopall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oftuamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Diff in %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdiffinperoffall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dinper")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Destination">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcallovipurposeopall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "destintion")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Call/Visit time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcallovtimeopall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "calovtime")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discussion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDiscussionall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Appointment">
                                                <ItemTemplate>
                                                    <asp:Label ID="nappdatall" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRemarksopall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
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
                            </asp:View>

                            <asp:View ID="ViewTransfer" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvClientTransfer" runat="server" AllowPaging="True" OnPageIndexChanging="gvProsClient_PageIndexChanging"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                        CssClass="table table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvslno0" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transfer To">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtrnsteam" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tteamdesc")) %>'
                                                        Width="160px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Client Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvClientNametrns" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clientname")) %>'
                                                        Width="160px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPerAddresstrns" runat="server" ForeColor="Black"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "caddress")) %>'
                                                        Width="160px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvphonetrns" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvemailtrns" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>

                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>


                        </asp:MultiView>

                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

