
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptApplicantInfo.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.RptApplicantInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

    .style24
    {
        width: 7px;
    }
    .style16
    {
        width: 99px;
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
    .style21
    {
        width: 13px;
    }
    .ddl
{
	border: 1px solid #7393B1;
	font-size: 11px;
	font-style: normal;
	background-color: #FFFFFF;
    text-align: left;
    font-weight: 700;
}
.ddl
{
	font-weight: normal;
	border-style: none;
	border-width:thick;
	font-size:12px;
}

    .style36
    {
        width: 372px;
    }
    
        .style37
        {
            width: 865px;
        }
        .style38
        {
            width: 268px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table style="width: 99%;">
        <tr>
            <td class="style12">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="EMPLOYEE PERSONAL  INFORMATION VIEW/EDIT" Width="500px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" ></asp:Label>
            </td>
            <td>
                                                                        <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td>
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
        </tr>
        </table>
          
                
      

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
            <table style="width:100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                    
                            <table style="width:100%; margin-bottom: 0px;">
                            <tr>
                                    <td class="style24">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Company :" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style16">
                                        <asp:TextBox ID="txtSComp" runat="server" CssClass="txtboxformat" 
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style21">
                                        <asp:ImageButton ID="ImageButton3" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnEmpList_Click" 
                                            style="width: 18px" />
                                    </td>
                                    <td class="style37">
                                        <asp:DropDownList ID="ddlCompany" runat="server" 
                                            Font-Bold="True" Font-Size="12px" Width="250px" 
                                            onselectedindexchanged="ddlCompany_SelectedIndexChanged" 
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCompany" runat="server" BackColor="White" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Blue" Visible="False" Width="250px"></asp:Label>
                                    </td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style38">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            <tr>
                                    <td class="style24">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Office :" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style16">
                                        <asp:TextBox ID="txtSOffice" runat="server" CssClass="txtboxformat" 
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style21">
                                        <asp:ImageButton ID="ImageButton2" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnEmpList_Click" 
                                            style="width: 18px" />
                                    </td>
                                    <td class="style37">
                                        <asp:DropDownList ID="ddlOffice" runat="server" 
                                            Font-Bold="True" Font-Size="12px" Width="250px" 
                                            onselectedindexchanged="ddlOffice_SelectedIndexChanged" 
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblOffice" runat="server" BackColor="White" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Blue" Visible="False" Width="250px"></asp:Label>
                                    </td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style38">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            <tr>
                                    <td class="style24">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Post" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style16">
                                        <asp:TextBox ID="txtDesSearch" runat="server" CssClass="txtboxformat" 
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style21">
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnEmpList_Click" 
                                            style="width: 18px" />
                                    </td>
                                    <td class="style37">
                                        <asp:DropDownList ID="ddlDesig" runat="server" 
                                            Font-Bold="True" Font-Size="12px" Width="250px" 
                                            onselectedindexchanged="ddlDesig_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblDesig" runat="server" BackColor="White" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Blue" Visible="False" Width="250px"></asp:Label>
                                        <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="12px" 
                                            Height="16px" onclick="lbtnOk_Click" 
                                            style="text-align: center; color: #FFFFFF; ">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style38">
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                
                                
                            </table>
                            </fieldset>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewAppInfo" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvappinfo" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" style="margin-right: 0px" Width="678px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialno" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvName" runat="server" Font-Size="11PX" 
                                                                style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>' 
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pre. Desig.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDesig" runat="server" Font-Size="11PX" 
                                                                style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "predesig")) %>' 
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pre.Company">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcomyany" runat="server" Font-Size="11PX" 
                                                                style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "precomp")) %>' 
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Experience">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvexperience" runat="server" Font-Size="11PX" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "exper")) %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Age">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvage" runat="server" Font-Size="11PX" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "age")).ToString("#, ##0;(#, ##0); ") %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Exp.Salary">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvExpSalary" runat="server" Font-Size="11PX" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "expsal")).ToString("#, ##0;(#, ##0); ") %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Pre.Salary">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPreSalary" runat="server" Font-Size="11PX" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "presal")).ToString("#, ##0;(#, ##0); ") %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Contact No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcontructno" runat="server" Font-Size="11PX" 
                                                                style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "conno")) %>' 
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Present Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpreaddress" runat="server" Font-Size="11PX" 
                                                                style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addres")) %>' 
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
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


