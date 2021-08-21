<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptBankStatement.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.RptBankStatement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Month</asp:Label>
                                        <asp:TextBox ID="txtfMonth" runat="server" CssClass=" inputDateBox "></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtfMonth_CalendarExtender" runat="server"
                                            Enabled="True" Format="yyyyMM" TargetControlID="txtfMonth"
                                            PopupButtonID="Image2">
                                        </cc1:CalendarExtender>
                                        <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="76" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>

                                        <div class="pull-left">
                                            <asp:LinkButton ID="lnkbtnShow0" runat="server" OnClick="lnkbtnShow_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label4" runat="server" CssClass="btn btn-info primaryBtn"
                                                    Text="Please wait . . . . . . ." Width="120px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>




                                </div>
                                <div class="form-group">
                                   <asp:CheckBox ID="chkBonus" runat="server" CssClass=" btn btn-primary  checkBox" Style="height: 25px !important;" Text="Festival Bonus" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <asp:GridView ID="gvbnkst" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvbnkst_PageIndexChanging" ShowFooter="True" Width="500px"
                                    OnRowDataBound="gvbnkst_RowDataBound">
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name  & Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "bnkname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ "<B>"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "bnkname")).Trim(): "")  +   "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "sectionname").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")).Trim(): "")
                                                                    %>'
                                                    Width="300px">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnetsal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="ViewBankWisw" runat="server">
                                <asp:GridView ID="gvbsbwise" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="500px"
                                    OnRowDataBound="gvbsbwise_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanged="gvbsbwise_PageIndexChanged">
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name  & Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbsbwiseDesc" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "bnkname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "companyname").ToString().Trim().Length>0 ?   
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "bnkname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ "<B>"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim(): "")  +   "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "sectionname").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "bnkname")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")).Trim(): "")
                                                                    %>'
                                                    Width="300px">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbsbamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
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





            <table style="width: 100%;">
                <%--<tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel4" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style47">
                                        &nbsp;
                                    </td>
                                    <td class="style15">
                                        <asp:Label ID="lblfrmdate" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Style="text-align: left" Text="Month:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style46">
                                        <asp:TextBox ID="txtfMonth" runat="server" CssClass="txtboxformat" MaxLength="6"
                                            TabIndex="9" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfMonth_CalendarExtender" runat="server" Enabled="True"
                                            Format="yyyyMM" PopupButtonID="Image2" TargetControlID="txtfMonth">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style24">
                                        <asp:Image ID="Image2" runat="server" Height="16" ImageUrl="~/Image/calender.png"
                                            Width="16" />
                                    </td>
                                    <td class="style48">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                            Style="color: #FFFFFF; text-align: left;" Text="Page Size:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style49">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Style="margin-left: 0px" TabIndex="10" Width="100px">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkbtnShow0" runat="server" BackColor="#003366" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Height="16px" OnClick="lnkbtnShow_Click" Style="text-align: center;" TabIndex="11"
                                            Width="50px">Ok</asp:LinkButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label4" runat="server" BackColor="Blue" BorderColor="White" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow" Style="text-align: left"
                                                    Text="Please wait . . . . . . ." Width="120px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style47">
                                        &nbsp;</td>
                                    <td class="style15">
                                        <asp:CheckBox ID="chkBonus" runat="server" BackColor="#003366" 
                                            BorderStyle="None" Font-Bold="True" Font-Size="12px" ForeColor="Yellow" 
                                            Text="Festival Bonus" Width="100px" />
                                    </td>
                                    <td class="style46">
                                        &nbsp;</td>
                                    <td class="style24">
                                        &nbsp;</td>
                                    <td class="style48">
                                        &nbsp;</td>
                                    <td class="style49">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="12"></td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

