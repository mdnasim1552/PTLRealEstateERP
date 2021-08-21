<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPaymentStatus.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPaymentStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

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
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-9 pading5px asitCol9">
                                            <asp:Label ID="lblProjectName" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>
                                            <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                           <asp:DropDownList ID="ddlProjectName" runat="server"  CssClass="ddlPage"  Width="350px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server" QueryPattern="Contains"
                                            TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>

                                            <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-9 pading5px asitCol9">
                                            <asp:Label ID="lblsupplier" runat="server" CssClass="lblTxt lblName" Text="Supplier:"></asp:Label>
                                            <asp:TextBox ID="txtSrcSupplier" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="imgbtnFindSupplier" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindSupplier_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass=" ddlPage" Width="350px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-9 pading5px asitCol9">
                                            <asp:Label ID="lbldatefrm" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>

                                            <asp:TextBox ID="txtFDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtFDate"></cc1:CalendarExtender>

                                            <asp:Label ID="lbldateto" runat="server" CssClass=" smLbl_to" Text="To:"></asp:Label>

                                            <asp:TextBox ID="txttodate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-9 pading5px asitCol9">

                                            <asp:Label ID="LblReqno0" runat="server" CssClass="lblTxt lblName" Text="MRR REF:"></asp:Label>
                                            <asp:TextBox ID="txtSrcMrfNo" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                            <asp:Label ID="lblPage0" runat="server" CssClass=" smLbl_to" Text="Size:"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage">
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
                                    </div>

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="vdaywisepurchase" runat="server">
                                   <asp:GridView ID="gvPayStatus" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                OnPageIndexChanging="gvPayStatus_PageIndexChanging" ShowFooter="True" Width="831px"
                OnRowDataBound="gvPayStatus_RowDataBound">
                <PagerSettings Position="Top" />

                <Columns>
                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Voucher No">
                        <ItemTemplate>
                            <asp:Label ID="lgvvoucher" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Voucher Date">
                        <ItemTemplate>
                            <asp:Label ID="lgvpaymentdate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheque No">
                        <ItemTemplate>
                            <asp:Label ID="lgvchequeno" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Bill No">
                        <ItemTemplate>
                            <asp:Label ID="lgvBillNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bill Date">
                        <ItemTemplate>
                            <asp:Label ID="lgvBilldate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdat")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Bill Ref">
                        <ItemTemplate>
                            <asp:Label ID="lgvBillref" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Project Desc.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvprojectdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                Width="200px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Material Name">
                        <ItemTemplate>
                            <asp:Label ID="lgvMaterials" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                Width="135px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>
                            <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Specification">
                        <ItemTemplate>
                            <asp:Label ID="lgvSpecifi" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label ID="lgvqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rate">
                        <ItemTemplate>
                            <asp:Label ID="lgvrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bill Amount">
                        <ItemTemplate>
                            <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                Style="text-align: right" Width="70px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Payment Amount">
                        <ItemTemplate>
                            <asp:Label ID="lgvpayAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFpayAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                Style="text-align: right" Width="70px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>




                    <asp:TemplateField HeaderText="Uncleared">
                        <ItemTemplate>
                            <asp:Label ID="lgvuncleared" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uncleared")) %>'
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
                        </asp:MultiView>
                    </div>
                </div>
            </div>

            <%--<tr>
                                    <td class="style49">
                                    </td>
                                    <td class="style50">
                                        <asp:Label ID="lblProjectName" runat="server" Font-Bold="True" Style="text-align: left;
                                            color: #FFFFFF;" Text="Project Name:" Width="100px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style51">
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="txtboxformat" Font-Bold="True"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style52">
                                        <asp:ImageButton ID="imgbtnFindProject" runat="server" Height="17px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="imgbtnFindProject_Click" Width="16px" />
                                    </td>
                                    <td class="style71">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" Font-Size="12px"
                                            Width="300px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server" QueryPattern="Contains"
                                            TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td class="style53">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" BorderColor="#000"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            OnClick="lbtnOk_Click" Style="text-align: center; height: 17px;" Width="50px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td class="style53">
                                    </td>
                                    <td class="style53">
                                    </td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style37">
                                    </td>
                                    <td class="style36">
                                        <asp:Label ID="lblsupplier" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: left;
                                            color: #FFFFFF;" Text="Supplier:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style56">
                                        <asp:TextBox ID="txtSrcSupplier" runat="server" CssClass="txtboxformat" Font-Bold="True"
                                            TabIndex="5" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style57">
                                        <asp:ImageButton ID="imgbtnFindSupplier" runat="server" Height="17px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="imgbtnFindSupplier_Click" TabIndex="6" Width="16px" />
                                    </td>
                                    <td class="style72">
                                        <asp:DropDownList ID="ddlSupplier" runat="server" Font-Bold="True" Font-Size="12px"
                                            Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style37">
                                    </td>
                                    <td class="style36">
                                        <asp:Label ID="lbldatefrm" runat="server" Font-Bold="True" Style="text-align: left;
                                            color: #FFFFFF;" Text="Date:" Width="100px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style56">
                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtFDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style57">
                                        <asp:Label ID="lbldateto" runat="server" Font-Bold="True" Style="text-align: right;
                                            color: #FFFFFF;" Text="To:" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style72">
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat" Font-Bold="False"
                                            Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txttodate" TodaysDateFormat="">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style37">
                                        &nbsp;
                                    </td>
                                    <td class="style36">
                                        <asp:Label ID="LblReqno0" runat="server" CssClass="style42" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: left" Text="MRR REF:" Width="70px"></asp:Label>
                                    </td>
                                    <td class="style56">
                                        <asp:TextBox ID="txtSrcMrfNo" runat="server" CssClass="txtboxformat" Font-Bold="True"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style57">
                                        <asp:Label ID="lblPage0" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right;
                                            color: #FFFFFF;" Text="Size:"></asp:Label>
                                    </td>
                                    <td class="style72">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Width="80px">
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
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>--%>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

