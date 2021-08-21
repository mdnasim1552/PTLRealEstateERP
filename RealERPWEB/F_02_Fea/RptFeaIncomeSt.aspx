<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptFeaIncomeSt.aspx.cs" Inherits="RealERPWEB.F_02_Fea.RptFeaIncomeSt" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        .style18
        {
            color: #FFFFFF;
            text-align: left;
        }
        .style59
        {
            width: 14px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
 
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
                <asp:Label ID="lblHeader" runat="server" BackColor="Blue" Font-Bold="True" 
                ForeColor="Yellow" style="font-weight: 700; color: #FFFF66; text-align: left" Text="PROJECT FEASIBILITY INCOME STATEMENT" 
                    Width="450px"></asp:Label>
            </td>
            <td class="style47">
                        <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td class="style44">
                &nbsp;</td>
            <td>
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" 
                    Font-Italic="False" Font-Underline="True" 
                    onclick="lnkPrint_Click" 
                    
                    style="  border-left-width: 2px; border-left-color: #ffff00;   text-align: center; color: #FFFFFF;" 
                    CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server"  BorderColor="Yellow" 
                             BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style58">
                                        <asp:Label ID="Label4" runat="server" CssClass="style50" Font-Bold="True" 
                                            Font-Size="12px" style="text-align: left" Text="Project Name:" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style57">
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style59">
                                        <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindProject_Click" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" 
                                            Font-Size="12px" Width="400px">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" onclick="lnkbtnSerOk_Click" 
                                            style="color: #FFFFFF; height: 17px; text-align: center;" Width="30px">Ok</asp:LinkButton>
                                    </td>
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
                        </asp:Panel>
                    </td>
                </tr>
                
                <tr>
                    <td>
                       
                           
                                <asp:GridView ID="gvFeaIncomeSt" runat="server" AutoGenerateColumns="False" 
                                    ShowFooter="True" 
                                    onrowdatabound="gvFeaIncomeSt_RowDataBound">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                      
                                         <asp:TemplateField HeaderText="Items Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgroupdesc" runat="server" AutoCompleteType="Disabled" 
                                                    BackColor="Transparent" BorderStyle="None" Font-size="11px" 
                                                      Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "infdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")).Trim(): "")  %>'   
                                                          Width="250px"></asp:Label>                                      
                                                  
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Orginal Value">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOamt" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orgamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revised Value">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTamt" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="% Orginal">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOpar" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orparcent")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="% Revised">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRpar" runat="server" BackColor="Transparent" 
                                                    BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rparcent")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                         
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerStyle HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            
                      
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

