<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptLoanStatus.aspx.cs" Inherits="RealERPWEB.F_24_CC.RptLoanStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });



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
            <div class="card mt-5">
                <div class="card-header">
                    <div class="row mt-3">
                       
                               

                                    <div class="col-lg-2 col-md-2 col-sm-6">
                                        <asp:Label ID="lblProject" runat="server" CssClass="form-label" Text="Project Name"></asp:Label>
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="ibtnFindProject_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="form-control" ></asp:TextBox>
                                         
                                    </div>
                                
                                    <div class="col-lg-3 col-md-3 col-sm-6 mt-3 mr-2">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-6 mt-3">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>


                                    <div class="col-lg-2 col-md-2 col-sm-6">
                                        <asp:Label ID="lblPage" runat="server" CssClass="form-label" Text="Page Size"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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

                                    <div class="col-lg-2 col-md-2 col-sm-6">
                                        <asp:Label ID="lblDate" runat="server" CssClass="form-label" Text="Add. No"></asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control inputTxt inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                    </div>
                                    <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn-danger btn primaryBtn d-none"></asp:Label>

                               

                            </div>
                       
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">

                            <asp:GridView ID="gvLoan" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvLoan_PageIndexChanging"
                                ShowFooter="True" Width="748px">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Client Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Loan Provider" FooterText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlnprovider" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lnprovdr")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" Font-Size="12px"
                                            ForeColor="White" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Loan Amt.">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFLnamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlnamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lnamt")).ToString("#, ##0;(#, ##0); ") %>'
                                                Width="80px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Received Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrcvdate" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Registration Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvregdate" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "regdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>


                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>
                        <asp:View ID="ViewGeneralLetter" runat="server">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:Label ID="lblletterinfo" runat="server" CssClass="lblName" Text="Letter Information:"
                                        Visible="False" Width="105px"></asp:Label>


                                    <asp:CheckBox ID="chekAss" runat="server" AutoPostBack="True"
                                        Text="Association :" CssClass="chkBoxControl btn-primary primaryBtn"
                                        OnCheckedChanged="chekAss_CheckedChanged" Width="120px" />
                                </div>


                            </div>
                            <asp:GridView ID="gvLetter" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="258px">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvletcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letcode")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkletter" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>' />
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Letter Information">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvteamdesc0" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                            <asp:Panel ID="panAss" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="right" class="style48">

                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="White"
                                                Text="Name :" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" Width="300px" BorderStyle="None"></asp:TextBox>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style48">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="White"
                                                Text="Address :" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAdd" runat="server" Width="300px"
                                                Style="margin-left: 0px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style48">

                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="White"
                                                Text="Mobile :" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMob" runat="server" Width="300px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style48">
                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="White"
                                                Text="Email :" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style48">&nbsp;</td>
                                        <td>
                                            <asp:RegularExpressionValidator ID="emailID" runat="server"
                                                ControlToValidate="txtEmail" ErrorMessage="Correct email add" ForeColor="White"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <div class="col-md-12">
                                <asp:Label ID="lblClientInfo" runat="server" Text="Client Information:"
                                    Visible="False" CssClass="btn btn-success primaryBtn"></asp:Label>
                                <div class="clearfix"></div>
                            </div>
                            <asp:GridView ID="gvClientLetter" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="gvClientLetter_PageIndexChanging" ShowFooter="True"
                                Width="614px">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkletterc" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>' />
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvletcodec" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Client Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclinetname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclientAddress" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paddress")) %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclientMob" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclientemail" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>' Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                            <asp:Panel ID="PanelSendMail" runat="server" Visible="False">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="style43">&nbsp;</td>
                                        <td class="style41">
                                            <asp:Label ID="lblForm" runat="server" CssClass="style34" Font-Bold="True"
                                                Font-Size="12px" Style="text-align: right" Text="From:" Width="80px"
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td class="style44">
                                            <asp:TextBox ID="txtfromEmail" runat="server" CssClass="txtboxformat"
                                                Width="700px" Visible="False"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbtnSend" runat="server" BackColor="#003366"
                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                Font-Size="12px" OnClick="lbtnSend_Click" Style="color: #FFFFFF; height: 17px;">Send</asp:LinkButton>
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
                                    <tr>
                                        <td class="style43">&nbsp;</td>
                                        <td class="style41">&nbsp;</td>
                                        <td class="style44">
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="txtboxformat"
                                                EnableViewState="False" Height="100px" TextMode="MultiLine" Width="700px"
                                                Visible="False"></asp:TextBox>

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
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table style="width: 100%;">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td></td>
                                    <td></td>
                                    <td class="style47"></td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style47">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td colspan="15">&nbsp;</td>
                                    <td colspan="4">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3"></td>
                                    <td colspan="24" valign="top"></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td colspan="6">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                    <td colspan="23">&nbsp;</td>
                                    <td colspan="15">&nbsp;</td>
                                    <td colspan="4">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="51"></td>
                                </tr>
                                <tr>
                                    <td colspan="51"></td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="ViewRegistration" runat="server">
                            <asp:GridView ID="gvRegis" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvRegis_PageIndexChanging"
                                ShowFooter="True" Width="605px">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Client Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclNamereg" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvProject" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Unit Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnitreg" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Received Token From Legal">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrtlegdept" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rtlegdept")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Received Certified From Legal">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrclegdept" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rclegdept")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Provided Token to Client">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvptclient" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ptclient")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Provided Certified to Client">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpcclient" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pcclient")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>
                        <asp:View ID="ViewMaintenance" runat="server">
                            <asp:Panel ID="Panel2" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="style35">&nbsp;</td>
                                        <td class="style27">
                                            <asp:Label ID="lblUnit" runat="server" CssClass="style34" Font-Bold="True"
                                                Font-Size="12px" Style="text-align: right" Text="Unit Name:" Width="80px"></asp:Label>
                                        </td>
                                        <td class="style28">
                                            <asp:TextBox ID="txtSrcUnit" runat="server" CssClass="txtboxformat"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="style32" valign="top">
                                            <asp:ImageButton ID="ibtnFindUnit" runat="server" Height="18px"
                                                ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindUnit_Click"
                                                Style="width: 18px" />
                                        </td>
                                        <td class="style40">
                                            <asp:DropDownList ID="ddlUnitName" runat="server" Font-Bold="True"
                                                Font-Size="12px" Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:GridView ID="gvadwrk" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvadwrk_PageIndexChanging"
                                OnRowDataBound="gvadwrk_RowDataBound" ShowFooter="True" Width="762px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modification Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmotype" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDate1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addate")) %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modification No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvadno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvgcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrate" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdisamt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Net Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvnetAmount" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
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
                        </asp:View>


                        <asp:View ID="ViewAddaDedTopSheet" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">

                                    <div class="form-horizontal">

                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblcustname" runat="server" CssClass="lblTxt lblName">Customer Name</asp:Label>
                                                <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass=" inputtextbox" TabIndex="14"></asp:TextBox>
                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="imgbtnFindCustomer" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindCustomer_Click" TabIndex="15"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>

                                            </div>
                                            <div class="col-md-4 pading5px">
                                                <asp:DropDownList ID="ddlCustName" runat="server"
                                                    CssClass="chzn-select form-control inputTxt" TabIndex="13" >
                                                </asp:DropDownList>
                                            </div>


                                            <div class="col-md-3 pading5px asitCol3">

                                                <asp:Label ID="lblreportlevel" runat="server" CssClass="smLbl_to" Text="Group"></asp:Label>

                                                <asp:DropDownList ID="ddlgroup" runat="server" CssClass="smDropDown inputTxt" TabIndex="6" style="width:55px;">
                                                    <asp:ListItem Value="2">Main</asp:ListItem>
                                                    <asp:ListItem Value="4">Sub-1</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="6">Sub-2</asp:ListItem>
                                                    <asp:ListItem  Value="9">Details</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>


                                        </div>

                                    </div>
                                </fieldset>


                                <asp:GridView ID="gvtopsheetmwrk" runat="server" AllowPaging="false" OnRowDataBound="gvtopsheetmwrk_RowDataBound"
                                    AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                    CssClass="table table-striped table-hover table-bordered grvContentarea" Width="671px">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of  Work/Item">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvworkdesc" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>






                                        <asp:TemplateField HeaderText="Demand Amount">

                                            <FooterTemplate>

                                                <asp:LinkButton ID="lbtnCalculation" runat="server" Font-Bold="True"
                                                    Font-Size="12px" CssClass="btn btn-danger primaryBtn" OnClick="lbtnCalculation_Click">Calculation</asp:LinkButton>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdemamt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "demamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Refund Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefamt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "refamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Discount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdisamt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Net Amount">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvAmt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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
                    </asp:MultiView>

                </div>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server">
                            <%--<table style="width: 100%;">
                                <tr>
                                    <td class="style35">&nbsp;</td>
                                    <td class="style27">
                                        <asp:Label ID="lblProject" runat="server" CssClass="style34" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: right" Text="Project Name:"
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style28">
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat"
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style32" valign="top">
                                        <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindProject_Click"
                                            Style="width: 18px" />
                                    </td>
                                    <td class="style40">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True"
                                            Font-Size="12px" Width="350px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" OnClick="lbtnOk_Click"
                                            Style="color: #FFFFFF; width: 19px; height: 17px;">Ok</asp:LinkButton>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style35">&nbsp;</td>
                                    <td class="style27">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="color: #FFFFFF; text-align: right;" Text="Page Size:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style28">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="105px">
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
                                    </td>
                                    <td class="style32" valign="top">
                                        <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Text="Date:"></asp:Label>
                                    </td>
                                    <td class="style40"></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>--%>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


