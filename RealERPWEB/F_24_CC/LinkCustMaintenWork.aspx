<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkCustMaintenWork.aspx.cs" Inherits="RealERPWEB.F_24_CC.LinkCustMaintenWork" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

        .style35
    {
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
            width: 8px;
        }
        .style58
        {
        }
        .style60
        {
            width: 1353px;
        }
        .style61
        {
            width: 122px;
        }
        .style67
        {
            height: 23px;
        }
        .style68
        {
            width: 8px;
            height: 23px;
        }
        .style69
        {
            width: 1353px;
            height: 23px;
        }
        .style70
        {
            width: 122px;
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript"  language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
   
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
  

 <table style="width: 98%;">
        <tr>
            <td class="style12">
                <asp:Label runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="CLIENT'S MODIFICATION INFORMATION VIEW/EDIT" Width="590px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" 
                    ID="lblHeaderTitle" ></asp:Label>
            </td>
            <td>
                                                    <asp:Label ID="lbljavascript" 
                    runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" 
                    onclick="lbtnPrint_Click" style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
      
                
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    
                    <td colspan="12">
                       <fieldset style="border:1px solid yellow;">
                        <legend style="font-size:12px;font-weight:bold;color:White;"><asp:Label ID="Label1" runat="server" Text="Client's Modification"></asp:Label></legend>
                            
                                <asp:Panel ID="Panel2" runat="server">
                                    <table style="width:100%;">
                                 
                                        <tr>
                                            <td class="style67">
                                                </td>
                                            <td class="style67">
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Height="16px" style="color: #FFFFFF; text-align: right;" Text="Date:" 
                                                    Width="80px"></asp:Label>
                                            </td>
                                            <td class="style67">
                                                <asp:Label ID="lblDate" runat="server" BackColor="White" Font-Size="12px" 
                                                    Width="85px"></asp:Label>
                                            </td>
                                            <td class="style68" valign="middle">
                                                <asp:Label ID="Label9" runat="server" CssClass="style16" Font-Bold="True" 
                                                    Font-Size="12px" ForeColor="White" Height="16px" style="TEXT-ALIGN: right" 
                                                    Text="Add.No" Width="40px"></asp:Label>
                                            </td>
                                            <td class="style69">
                                                <asp:Label ID="lblCurNo1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: left; background-color: #FFFFFF;" 
                                                    Width="48px"></asp:Label>
                                                <asp:Label ID="lblCurNo2" runat="server" BackColor="White" Font-Bold="True" 
                                                    Font-Size="12px" 
                                                    style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: left; background-color: #FFFFFF;" 
                                                    Width="35px"></asp:Label>
                                            </td>
                                            <td class="style70">
                                                </td>
                                            <td class="style67">
                                                </td>
                                            <td class="style67">
                                                </td>
                                            <td class="style67">
                                                </td>
                                            <td class="style67">
                                                </td>
                                            <td class="style67">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="style58">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Height="16px" style="color: #FFFFFF; text-align: right;" Text="Project Name:" 
                                                    Width="80px"></asp:Label>
                                            </td>
                                            <td class="style35">
                                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" Width="80px" 
                                                    TabIndex="3"></asp:TextBox>
                                            </td>
                                            <td class="style57" valign="middle">
                                                <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" 
                                                    ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindProject_Click" 
                                                    TabIndex="4" />
                                            </td>
                                            <td class="style60">
                                                <asp:Label ID="lblProjectdesc" runat="server" BackColor="White" 
                                                    Font-Size="12px" ForeColor="Blue" Height="16px" Width="300px"></asp:Label>
                                            </td>
                                            <td class="style61">
                                                &nbsp;</td>
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
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td class="style58">
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Height="16px" style="color: #FFFFFF; text-align: right;" Text=" Unit Name:" 
                                                    Width="80px"></asp:Label>
                                            </td>
                                            <td class="style35">
                                                <asp:TextBox ID="txtsrchUnitName" runat="server" CssClass="txtboxformat" 
                                                    Width="80px" TabIndex="5"></asp:TextBox>
                                            </td>
                                            <td class="style57" valign="middle">
                                                <asp:ImageButton ID="ibtnFindUnitName" runat="server" Height="18px" 
                                                    ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindUnitName_Click" 
                                                    TabIndex="6" />
                                            </td>
                                            <td class="style60">
                                                <asp:Label ID="lblUnitName" runat="server" BackColor="White" Font-Size="12px" 
                                                    ForeColor="Blue" Height="16px" Width="300px"></asp:Label>
                                            </td>
                                            <td class="style61">
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
               </fieldset>
                        
                       </td>
                                 
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:GridView ID="gvAddWork" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" Width="543px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" ForeColor="Black" 
                                            Height="16px" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvGcodAdd" runat="server" ForeColor="Black" Height="16px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lgdescw" runat="server" ForeColor="Black" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvqty" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRate" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvamt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </FooterTemplate>
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
                <tr>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

