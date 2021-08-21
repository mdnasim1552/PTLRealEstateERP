<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LandBgdPrjAna.aspx.cs" Inherits="RealERPWEB.F_01_LPA.LandBgdPrjAna" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style50
        {
            color: white;
        }
        .txtboxformat
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 12px;
            font-weight: normal;
            margin-right: 0px;
            text-align: left;
            margin-left: 0px;
        }
        .style57
        {
            width: 12px;
        }
        .style58
        {
            width: 89px;
        }
        .style64
        {
            width: 8px;
        }
        .style65
        {
            width: 379px;
        }
        .style66
        {
            width: 69px;
        }
        .style69
        {
            width: 40px;
        }
        .style70
        {
            width: 4px;
        }
        .style71
        {
            width: 164px;
        }
        .style72
        {
            width: 56px;
        }
        .style73
        {
            width: 154px;
        }
        .style74
        {
            width: 55px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

      <script language="javascript" type="text/javascript">
          $(document).ready(function () {
              //For navigating using left and right arrow of the keyboard
              Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
          });
          function pageLoaded() {

              $("input, select").bind("keydown", function (event) {
                  var k1 = new KeyPress();
                  k1.textBoxHandler(event);
              });

          };
      </script>
    <table style="width: 912px">
        <tr>
            <td class="style43">
                <asp:Label ID="lblHeader" runat="server" BackColor="Blue" Font-Bold="True" ForeColor="Yellow"
                    Style="font-weight: 700; color: #FFFF66; text-align: left" Text="PROJECT FEASIBILITY"
                    Width="450px"></asp:Label>
            </td>
            <td class="style47">
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style44">
                &nbsp;
            </td>
            <td>
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" Font-Italic="False"
                    Font-Underline="True" OnClick="lnkPrint_Click" Style="border-left-width: 2px;
                    border-left-color: #ffff00; text-align: center; color: #FFFFFF;" CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style70">
                                        &nbsp;
                                    </td>
                                    <td class="style58">
                                        <asp:Label ID="Label4" runat="server" CssClass="style50" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Text="Project Name:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style57">
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style64">
                                        <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ibtnFindProject_Click" TabIndex="1" />
                                    </td>
                                    <td class="style65">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" Font-Size="12px"
                                            Width="400px" TabIndex="2"></asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" BackColor="White" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="Blue" Height="16px" 
                                            Visible="False" Width="400px"></asp:Label>
                                    </td>
                                    <td class="style66">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" Height="16px" OnClick="lnkbtnSerOk_Click" 
                                            Style="color: #FFFFFF; text-align: center;" TabIndex="3" Width="48px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style69">
                                        &nbsp;
                                        </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style73">
                                        &nbsp;
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="14px" ForeColor="White"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style70">
                                        &nbsp;</td>
                                    <td class="style58">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Style="color: #FFFFFF;
                                            text-align: right;" Text="Page Size:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style57">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="80px">
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
                                    <td class="style64">
                                        &nbsp;</td>
                                    <td class="style65">
                                        <asp:Label ID="lblConSFT" runat="server" BackColor="White" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Blue" Height="16px" Visible="False" Width="200px"></asp:Label>
                                    </td>
                                    <td class="style66">
                                        &nbsp;</td>
                                    <td class="style69">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style73">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            
                            <asp:View ID="ViewLandAnlysis" runat="server">
                                <table style="width: 100%;">
                                <tr>
                                <td>
                                
                                 <asp:Panel ID="PnlResInput" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Visible="false">
                                            <table style="width:100%;">
                                     <tr>
                                        <td class="style74">
                                            <asp:CheckBox ID="chkAllRes" runat="server" AutoPostBack="True" 
                                                Font-Bold="True" Font-Size="12px" ForeColor="#660033" 
                                                oncheckedchanged="chkAllSInf_CheckedChanged" 
                                                style="color: #FFFFFF; text-align: right;" Text="Show All" Width="100px" />
                                         </td>
                                        <td class="style71">
                                            <asp:Label ID="lblTitle2" runat="server" BackColor="Blue" Font-Bold="True" 
                                                Font-Size="14px" ForeColor="Yellow" 
                                                style="font-weight: 700; color: #FFFF66; text-align: left" 
                                                Text="Resource Rate Input &amp; Report" Width="303px"></asp:Label>
                                        </td>
                                        <td class="style57">
                                            <asp:TextBox ID="txtSearchItem" runat="server" BorderStyle="None" Width="66px"></asp:TextBox>
                                        </td>
                                        <td class="style72">
                                         <asp:LinkButton ID="lbtnSelectFloorRes" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" Height="16px" OnClick="lbtnSelectFloorRes_Click" 
                                            Style="color: #FFFFFF; text-align: center;" TabIndex="3" Width="52px">Show</asp:LinkButton>
                                           
                                        </td>
                                        <td class="style147">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td class="style147">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    </table>
                                    </asp:Panel>
                                    </td>
                                </tr>
                                    <tr>
                                        <td colspan="12">
                                           <asp:GridView ID="gvResInfo" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" onpageindexchanging="gvResInfo_PageIndexChanging" 
                                                PageSize="20" Width="16px" ShowFooter="True">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                   
                                                     <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IRes Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvIResCod" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>' 
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResCod" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' 
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Description">
                                                        
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvIsirdes" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>' 
                                                                Width="190px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Description of Resource">
                                                        <FooterTemplate>
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    
                                                                    <td>
                                                                        <asp:LinkButton ID="lbtnUpdateResRate" runat="server" Font-Bold="True" 
                                                                            Font-Size="12px" ForeColor="White" onclick="lbtnUpdateResRate_Click" 
                                                                            style="text-align: left; height: 17px;" 
                                                                            Width="80px">Update Rate</asp:LinkButton>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResDesc" runat="server" Font-Bold="True" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                                Width="300px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResUnit" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Est.Qty">
                                                      <%--  <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnSameValue" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" onclick="lbtnSameValue_Click" style="text-align: center;" 
                                                                Width="85px">Put Same Val</asp:LinkButton>
                                                        </FooterTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvResQty" runat="server" BorderColor="#99CCFF" Font-Size="11px"
                                                                BorderStyle="Solid" BorderWidth="0px" style="text-align:right;background-color:Transparent" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="85px" ></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Res. Rate">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvResRat" runat="server" BorderColor="#99CCFF" Font-Size="11px"
                                                                BorderStyle="Solid" BorderWidth="0px" style="text-align:right;background-color:Transparent" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnResTotal" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" onclick="lbtnResTotal_Click" style="text-align: center;" 
                                                                Width="50px">Total :</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTResAmt" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvTResAmtFooter" runat="server" Font-Size="12px" ForeColor="White" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" ForeColor="Black" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                    Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td class="style74">
                                            &nbsp;</td>
                                        <td class="style71">
                                            &nbsp;</td>
                                        <td class="style57">
                                            &nbsp;</td>
                                        <td class="style72">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
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
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
