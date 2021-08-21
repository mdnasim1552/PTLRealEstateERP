<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMisSCollTAActual.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptMisSCollTAActual" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


        .style33
        {
            width: 51px;
        }
        .style38
        {
            color: #FFFFFF;
            height: 17px;
        }
        .style34
        {
            width: 43px;
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
        .txtboxformat
{
	border-style: none;
    border-color: inherit;
    border-width: medium;
    font-size: 11px;
	    font-weight: normal;
	margin-right: 0px;
}
        .style32
        {
            width: 12px;
        }
        .style37
        {
            width: 103px;
        }
        .style40
        {
            width: 8px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table style="width:63%;">
        <tr>
            <td class="style35">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="MONTH WISE SALES & COLLECTION TARGET" Width="594px"
                   STYLE="border-bottom:1px solid blue;border-top:1px solid blue;" ></asp:Label>
            </td>
            <td class="style39">
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
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
                    onclick="lbtnPrint_Click" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
                
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width:100%;">
                                <tr>
                                    <td align="left" class="style33">
                                        <asp:Label ID="Label5" runat="server" CssClass="style38" Font-Bold="True" 
                                            Font-Size="12px" style="text-align: LEFT" Text="From.:" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style34">
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="txtboxformat" 
                                            Font-Bold="True" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat="">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style32">
                                        <asp:Label ID="Label6" runat="server" CssClass="style38" Font-Bold="True" 
                                            Font-Size="12px" style="text-align: right" Text="To:"></asp:Label>
                                    </td>
                                    <td class="style37">
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat" 
                                            Font-Bold="True" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat="">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style40">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="style38" 
                                            Font-Bold="True" Font-Size="12px" onclick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </td>
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
                    <td colspan="12">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <asp:GridView ID="gvSalVsColl" runat="server" AutoGenerateColumns="False" 
                                    onrowdatabound="gvSalVsColl_RowDataBound" ShowFooter="True" Width="531px">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdeptcode" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcode")) %>' 
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDepartment" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    style="text-align: Left; background-color: Transparent" 
                                                   
                                                   
                                                   
                                                   
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "deptmname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "deptsname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "deptmname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "deptsname")).Trim()+ "</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "deptname").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "deptsname")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")).Trim(): "")
                                                                    %>' 
                                                                
                                                   
                                                   
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdesignation" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    style="text-align: left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>' 
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Monthly Sale(Target)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmonsalamt" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    style="text-align: right; background-color: Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tmsaleamt")).ToString("#,##0;-#,##0; ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        
                                         <asp:TemplateField HeaderText="Monthly Sale(Actual)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamonsalamt" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    style="text-align: right; background-color: Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamsaleamt")).ToString("#,##0;-#,##0; ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Monthly Collection(Target)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmoncollamt" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    style="text-align: right; background-color: Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tmcollamt")).ToString("#,##0;-#,##0; ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Monthly Collection(actual)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamoncollamt" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    style="text-align: right; background-color: Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamcollamt")).ToString("#,##0;-#,##0; ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                       
                                        <asp:TemplateField HeaderText="Last Month Position &lt;br&gt; Sales">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpmonsalamt" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    style="text-align: right; background-color: Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamonsaleamt")).ToString("#,##0;-#,##0; ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Month Position &lt;br&gt; Collection">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpmoncollamt" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    style="text-align: right; background-color: Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamoncollamt")).ToString("#,##0;-#,##0; ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
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

