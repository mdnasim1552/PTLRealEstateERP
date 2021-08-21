<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="FinPayment.aspx.cs" Inherits="RealERPWEB.F_03_Fin.FinPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />

    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />

    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

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
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">
                                            <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Bank Name"></asp:Label>
                                            <asp:Label ID="lblBankmDesc" runat="server" CssClass="lblTxt lblName" Visible="False" Width="350px"></asp:Label>

                                             <asp:TextBox ID="txtSrcBank" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindBank" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindBank_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            <asp:DropDownList ID="ddlBankName" runat="server" Width="500px" CssClass="ddlPage" TabIndex="2"></asp:DropDownList>
                                            <asp:Label ID="lblBankdesc" runat="server" CssClass="inputTxt inpPixedWidth" Visible="False" Width="350px"></asp:Label>
                                            <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>
                                            <asp:Label ID="lmsg" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                        </div>
                                    </div>


                                <%--    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">
                                           
                                        </div>
                                    </div>--%>
                                </asp:Panel>
                            </div>
                        </fieldset>
                        <div class="table table-responsive">
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="ViewPersonal" runat="server">

                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">
                                            <asp:Label ID="lperInfo0" runat="server" CssClass="lblTxt lblName" Text="Payment Information" Width="610px"></asp:Label>
                                            <asp:Label ID="lblAcAmt" runat="server" CssClass="lblTxt lblName" Visible="False"></asp:Label>
                                        </div>
                                    </div>

                                    <%--<tr>
                                        <td>
                                            <asp:Label ID="lperInfo0" runat="server" Font-Bold="True" Font-Size="18px"
                                                ForeColor="#660066" Style="text-align: left; color: #FFFF99;"
                                                Text="Payment Information" Width="610px"></asp:Label>
                                            <asp:Label ID="lblAcAmt" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>--%>

                                    <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="392px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" ForeColor="Black"
                                                        Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvGcod" runat="server" ForeColor="Black" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalCost" runat="server" Font-Bold="True"
                                                        Font-Size="12px" ForeColor="White" OnClick="lbtnTotalCost_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lFinalUpdateCost" runat="server" Font-Bold="True"
                                                        Font-Size="12px" ForeColor="White" OnClick="lFinalUpdateCost_Click"> Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgdesc" runat="server" ForeColor="Black"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvuamt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks"))%>'
                                                        Width="100px"></asp:TextBox>
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
                                    
                                  <asp:Panel ID="Panel3" runat="server" BorderColor="Yellow" BorderStyle="Solid" Visible="false" >

                                     <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">
                                            <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName" Text="First Ins. Date:" Width="120px"></asp:Label>
                                              <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inpPixedWidth" Width="80px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                             <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Text="Total Installement:" Width="120px"></asp:Label>
                                              <asp:TextBox ID="txtTInstall" runat="server" CssClass="inputTxt inpPixedWidth" Width="100px"></asp:TextBox>

                                                <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Text="Duration:"  Width="80px"></asp:Label>

                                             <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="True" CssClass="ddlPage" 
                                                                Font-Bold="True" Font-Size="12px" Width="120px">
                                                                <asp:ListItem Value="1">1 Month</asp:ListItem>
                                                                <asp:ListItem Value="2">2 Month</asp:ListItem>
                                                                <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                                                <asp:ListItem Value="4">4 Month</asp:ListItem>
                                                                <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                                                <asp:ListItem Value="6">6 Month</asp:ListItem>
                                                                <asp:ListItem Value="7">7  Month</asp:ListItem>
                                                                <asp:ListItem Value="8">8  Month</asp:ListItem>
                                                                <asp:ListItem Value="9">9  Month</asp:ListItem>
                                                                <asp:ListItem Value="10">10  Month</asp:ListItem>
                                                                <asp:ListItem Value="11">11  Month</asp:ListItem>
                                                            </asp:DropDownList>

                                            <asp:LinkButton ID="lbtnGenerate" runat="server"  CssClass="btn btn-primary "  OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>
                                        </div>
                                         </div>
                                  </asp:Panel>
                                    <%--<tr>
                                        <td colspan="2" class="style62">
                                            <asp:Panel ID="Panel3" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                                BorderWidth="1px">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style33">&nbsp;</td>
                                                        <td class="style33">
                                                            <asp:Label ID="Label10" runat="server" CssClass="style23"
                                                                Text="First Ins. Date:" Width="120px"></asp:Label>
                                                        </td>
                                                        <td class="style33">
                                                            <asp:TextBox ID="txtdate" runat="server" CssClass="txtboxformat"
                                                                Font-Bold="True" Width="80px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                                        </td>
                                                        <td class="style17">
                                                            <asp:Label ID="Label7" runat="server" CssClass="style23"
                                                                Text="Total Installement:" Width="120px"></asp:Label>
                                                        </td>
                                                        <td class="style18">
                                                            <asp:TextBox ID="txtTInstall" runat="server" CssClass="txtboxformat"
                                                                Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td class="style19">
                                                            <asp:Label ID="Label8" runat="server" CssClass="style23" Text="Duration:"
                                                                Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="style21">
                                                            <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="True"
                                                                Font-Bold="True" Font-Size="12px" Width="120px">
                                                                <asp:ListItem Value="1">1 Month</asp:ListItem>
                                                                <asp:ListItem Value="2">2 Month</asp:ListItem>
                                                                <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                                                <asp:ListItem Value="4">4 Month</asp:ListItem>
                                                                <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                                                <asp:ListItem Value="6">6 Month</asp:ListItem>
                                                                <asp:ListItem Value="7">7  Month</asp:ListItem>
                                                                <asp:ListItem Value="8">8  Month</asp:ListItem>
                                                                <asp:ListItem Value="9">9  Month</asp:ListItem>
                                                                <asp:ListItem Value="10">10  Month</asp:ListItem>
                                                                <asp:ListItem Value="11">11  Month</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtnGenerate" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="style22"
                                                                Font-Bold="True" Font-Size="12px" OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>--%>


                                            <asp:Panel ID="PanelAddIns" runat="server" BorderColor="Yellow"  Visible="False">
                                                 <div class="form-group">
                                                        <div class="col-md-10  pading5px  asitCol10">
                                                            <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName" Text="Installement:" Width="120px"></asp:Label>
                                                              <asp:TextBox ID="txtsrchInstallment" runat="server" CssClass="inputTxt inpPixedWidth" Width="100px"></asp:TextBox>
                                                            <asp:ImageButton ID="ibtnFindInstallment" runat="server" Height="18px"
                                                                ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindInstallment_Click" />
                                                            <asp:DropDownList ID="ddlInstallment" runat="server" CssClass="ddlPage" Width="200px"> </asp:DropDownList>

                                                             <asp:LinkButton ID="lbtnAddInstallment" runat="server"  CssClass="btn primaryBtn okBtn" OnClick="lbtnAddInstallment_Click">Add</asp:LinkButton>
                                                        </div>
                                                    </div>

                                                <%--<table style="width: 100%;">
                                                    <tr>
                                                        <td class="style33">&nbsp;</td>
                                                        <td class="style21">
                                                            <asp:Label ID="Label9" runat="server" CssClass="style23" Text="Installement:"
                                                                Width="120px"></asp:Label>
                                                        </td>
                                                        <td class="style18">
                                                            <asp:TextBox ID="txtsrchInstallment" runat="server" CssClass="txtboxformat"
                                                                Width="100px"></asp:TextBox>
                                                        </td>
                                                        <td class="style58">
                                                            <asp:ImageButton ID="ibtnFindInstallment" runat="server" Height="18px"
                                                                ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindInstallment_Click" />
                                                        </td>
                                                        <td class="style33">
                                                            <asp:DropDownList ID="ddlInstallment" runat="server" Font-Bold="True"
                                                                Font-Size="12px" Width="200px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtnAddInstallment" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="style22"
                                                                Font-Bold="True" Font-Size="12px" OnClick="lbtnAddInstallment_Click">Add</asp:LinkButton>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>--%>
                                            </asp:Panel>
                                       


                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="Panel5" runat="server">

                                                 <div class="form-group">
                                                        <div class="col-md-10  pading5px  asitCol10">

                                                            <asp:Label ID="lPays" runat="server" CssClass="lblTxt lblName"  Text="Payment Shedule" Width="180px"></asp:Label>
                                                             <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True"  CssClass="checkbox"
                                                                OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment"
                                                                Width="140px" />

                                                              <asp:CheckBox ID="chkAddIns" runat="server" AutoPostBack="True" CssClass="checkbox" OnCheckedChanged="chkAddIns_CheckedChanged" Text="Add.Installment"
                                                                Width="140px" />
                                                   </div>
                                                    </div>


                                                <%--<table style="width: 100%;">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td class="style61">
                                                            <asp:Label ID="lPays" runat="server" CssClass="style22" Font-Bold="True"
                                                                Font-Size="18px" ForeColor="#FFFF99" Height="26px" Style="text-align: left"
                                                                Text="Payment Shedule" Width="180px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True"
                                                                CssClass="style22" Font-Bold="True" ForeColor="White"
                                                                OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment"
                                                                Width="140px" />
                                                        </td>
                                                        <td class="style64">
                                                            <asp:CheckBox ID="chkAddIns" runat="server" AutoPostBack="True"
                                                                CssClass="style22" Font-Bold="True" ForeColor="White"
                                                                OnCheckedChanged="chkAddIns_CheckedChanged" Text="Add.Installment"
                                                                Width="140px" />
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td class="style63">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>--%>
                                            </asp:Panel>
                                        
                                            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                OnRowDeleting="gvPayment_RowDeleting" ShowFooter="True" Width="16px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" ForeColor="Black"
                                                                Height="16px" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCode3" runat="server" ForeColor="Black" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" />
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcResDesc2" runat="server" ForeColor="Black"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lUpdatpayment" runat="server" Font-Bold="True"
                                                                Font-Size="12px" ForeColor="White" OnClick="lUpdatpayment_Click"
                                                                Style="text-decaration: none;">Update Payment Info</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvDate" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="11px" Height="20px"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                Format="dd-MMM-yyyy" TargetControlID="txtgvDate"></cc1:CalendarExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lTotalPayment" runat="server" Font-Bold="True"
                                                                Font-Size="12px" ForeColor="White" OnClick="lTotalPayment_Click">Total Payment</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvAmt" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;-#,##0; ") %>'
                                                                Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lfAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
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
                            <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" QueryPattern="Contains" TargetControlID="ddlBankName"></cc1:ListSearchExtender>
                        </div>
                    </div>
                </div>
            </div>

            <%--<tr>
                                    <td></td>
                                    <td class="style50">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Bank Name:"
                                            Style="color: #FFFFFF"></asp:Label>
                                    </td>
                                    <td class="style35"></td>
                                    <td class="style55">
                                        <asp:Label ID="lblBankmDesc" runat="server" BackColor="White"
                                            Font-Size="12px" ForeColor="Blue" Visible="False" Width="350px"></asp:Label>
                                        <asp:Label ID="lmsg" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="backcolor=&quot; red&quot;" BackColor="Red"></asp:Label>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>--%>
            <%--  <tr>
                                    <td></td>
                                    <td class="style50">
                                        <asp:TextBox ID="txtSrcBank" runat="server" CssClass="txtboxformat"
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style35">
                                        <asp:ImageButton ID="ibtnFindBank" runat="server" Height="18px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindBank_Click"
                                            TabIndex="1" />
                                    </td>
                                    <td valign="top" class="style55">
                                        <asp:DropDownList ID="ddlBankName" runat="server"
                                            Font-Bold="True" Font-Size="12px" Width="350px" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblBankdesc" runat="server" BackColor="White"
                                            Font-Size="12px" ForeColor="Blue" Height="16px" Visible="False"
                                            Width="350px"></asp:Label>
                                        <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="12px"
                                            OnClick="lbtnOk_Click" Style="color: #FFFFFF; text-align: center;" BackColor="#003366"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Width="45px"
                                            TabIndex="3">Ok</asp:LinkButton>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



