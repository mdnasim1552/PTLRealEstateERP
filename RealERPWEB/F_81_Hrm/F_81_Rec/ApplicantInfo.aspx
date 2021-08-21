<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ApplicantInfo.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.ApplicantInfo" %>


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
            <table style="width:100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="#FFCC00" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width:100%; margin-bottom: 0px;">
                            <tr>
                                    <td class="style24">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Company" 
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
                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Applicant List" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmpSrc" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnEmpList" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnEmpList_Click" 
                                            style="width: 18px" />
                                    </td>
                                    <td class="style38">
                                        <asp:DropDownList ID="ddlEmpName" runat="server" Font-Bold="True" 
                                            Font-Size="12px" Width="250px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblEmpName" runat="server" BackColor="White" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Blue" Visible="False" Width="250px"></asp:Label>
                                    </td>
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
                                            ForeColor="White" style="text-align: right" Text="Office" 
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
                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right; margin-bottom: 0px;" 
                                            Text="Information " Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInformation" runat="server" CssClass="txtboxformat" 
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnInformation" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnInformation_Click" 
                                            style="width: 18px" />
                                    </td>
                                    <td class="style38">
                                        <asp:DropDownList ID="ddlInformation" runat="server" Font-Bold="True" 
                                            Font-Size="12px" Width="250px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblInformation" runat="server" BackColor="White" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="Blue" Visible="False" 
                                            Width="250px"></asp:Label>
                                    </td>
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
                                    </td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                            Height="16px" onclick="lbtnOk_Click" 
                                            style="text-align: center; color: #FFFFFF;" Width="40px">Ok</asp:LinkButton>
                                    </td>
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
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewPersonal" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="831px" 
                                                onrowdatabound="gvPersonalInfo_RowDataBound">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="12px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCode" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcResDesc1" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvgph" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>' 
                                                                Width="2px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle Font-Bold="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvgval" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lUpdatPerInfo" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" onclick="lUpdatPerInfo_Click" 
                                                                style="text-decaration:none;">Update Personal Info</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                          
                                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" 
                                                                BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>' 
                                                                Width="510px"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlval" runat="server" Width="518px" 
                                                                BackColor="#CAE4FF" >
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                             <asp:View ID="ViewDegree" runat="server">
                                 <table style="width:100%;">
                                     <tr>
                                         <td>
                                             <asp:GridView ID="gvDegree" runat="server" AutoGenerateColumns="False" 
                                                 ShowFooter="True" Width="831px">
                                                 <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                 <Columns>
                                                     <asp:TemplateField HeaderText="Sl.No.">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" 
                                                                 style="text-align: right" 
                                                                 Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Code">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvItmCode" runat="server" Height="16px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                                                 Width="49px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Degree Name">
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="txtgcvDegree" runat="server"  BackColor="Transparent" 
                                                                 BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "degree")) %>' 
                                                                 Width="150px"></asp:TextBox>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="Left" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Group">
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="txtgvGroup" runat="server"  BackColor="Transparent" 
                                                                 BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "egroup")) %>' 
                                                                 Width="100px"></asp:TextBox>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="Left" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Institution">
                                                         <FooterTemplate>
                                                             <asp:LinkButton ID="lUpdateDegree" runat="server" Font-Bold="True" 
                                                                 Font-Size="12px" ForeColor="White" onclick="lUpdateDegree_Click" 
                                                                 style="text-decaration:none;">Update</asp:LinkButton>
                                                         </FooterTemplate>
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="txtgvInstituee" runat="server"  BackColor="Transparent" 
                                                                 BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "institute")) %>' 
                                                                 Width="300px"></asp:TextBox>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="Left" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Result">
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="txtgvResult" runat="server"  BackColor="Transparent" 
                                                                 BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "result")) %>' 
                                                                 Width="80px"></asp:TextBox>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="Left" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Passing Year">
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="txtgvPass" runat="server"  BackColor="Transparent" 
                                                                 BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pyear")) %>' 
                                                                 Width="80px"></asp:TextBox>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="Left" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     
                                                     <asp:TemplateField HeaderText="Type" Visible="False">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvgval" runat="server" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                         </ItemTemplate>
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
                             <asp:View ID="ViewCompany" runat="server">
                                 <table style="width:100%;">
                                     <tr>
                                         <td>
                                             <asp:GridView ID="gvEmpRec" runat="server" AutoGenerateColumns="False" 
                                                 ShowFooter="True" Width="831px">
                                                 <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                 <Columns>
                                                     <asp:TemplateField HeaderText="Sl.No.">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" 
                                                                 style="text-align: right" 
                                                                 Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Code">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvItmCode" runat="server" Height="16px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                                                 Width="49px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Company Name">
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="txtgcvCompany" runat="server" BackColor="Transparent" 
                                                                 BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comname")) %>' 
                                                                 Width="300px"></asp:TextBox>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="Left" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Designation">
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="txtgvDesig" runat="server" BackColor="Transparent" 
                                                                 BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>' 
                                                                 Width="300px"></asp:TextBox>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="Left" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Service Duration">
                                                         <FooterTemplate>
                                                             <asp:LinkButton ID="lUpdateEmprecord" runat="server" Font-Bold="True" 
                                                                 Font-Size="12px" ForeColor="White" onclick="lUpdateEmprecord_Click" 
                                                                 style="text-decaration:none;">Update</asp:LinkButton>
                                                         </FooterTemplate>
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="txtgvesDuration" runat="server" BackColor="Transparent" 
                                                                 BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "duration")) %>' 
                                                                 Width="100px"></asp:TextBox>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="Left" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                    
                                                     <asp:TemplateField HeaderText="Type" Visible="False">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvgval" runat="server" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                         </ItemTemplate>
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
                             <asp:View ID="ViewAssociation" runat="server">
                                 <table style="width:100%;">
                                     <tr>
                                         <td>
                                             <asp:GridView ID="gvAssocia" runat="server" AutoGenerateColumns="False" 
                                                 ShowFooter="True" Width="701px">
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
                                                     <asp:TemplateField HeaderText="Code">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvItmCode" runat="server" Height="16px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                                                 Width="49px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Organization Name">
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="txtgcvOrgName" runat="server" BackColor="Transparent" 
                                                                 BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orgname")) %>' 
                                                                 Width="300px"></asp:TextBox>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="Left" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Position">
                                                         <FooterTemplate>
                                                             <asp:LinkButton ID="lUpdateEmpAssocia" runat="server" Font-Bold="True" 
                                                                 Font-Size="12px" ForeColor="White" onclick="lUpdateEmpAssocia_Click" 
                                                                 style="text-decaration:none;">Update</asp:LinkButton>
                                                         </FooterTemplate>
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="txtgvPostion" runat="server" BackColor="Transparent" 
                                                                 BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "position")) %>' 
                                                                 Width="300px"></asp:TextBox>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                         <ItemStyle HorizontalAlign="Left" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                    
                                                     <asp:TemplateField HeaderText="Type" Visible="False">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvgval" runat="server" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                         </ItemTemplate>
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
                              <asp:View ID="ViewRef" runat="server">
                                  <asp:GridView ID="gvRef" runat="server" AutoGenerateColumns="False" 
                                      ShowFooter="True" Width="831px">
                                      <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                      <Columns>
                                          <asp:TemplateField HeaderText="Sl.No.">
                                              <ItemTemplate>
                                                  <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px" 
                                                      style="text-align: right" 
                                                      Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                              </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Code">
                                              <ItemTemplate>
                                                  <asp:Label ID="lblgvItmCode" runat="server" Height="16px" 
                                                      Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                                      Width="49px"></asp:Label>
                                              </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText=" Name">
                                              <ItemTemplate>
                                                  <asp:TextBox ID="txtgcvName" runat="server" BackColor="Transparent" 
                                                      BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                      Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refname")) %>' 
                                                      Width="150px"></asp:TextBox>
                                              </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Center" />
                                              <ItemStyle HorizontalAlign="Left" />
                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Organazation">
                                              <ItemTemplate>
                                                  <asp:TextBox ID="txtgvOrgname" runat="server" BackColor="Transparent" 
                                                      BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                      Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orgname")) %>' 
                                                      Width="200px"></asp:TextBox>
                                              </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Center" />
                                              <ItemStyle HorizontalAlign="Left" />
                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Designation">
                                              <FooterTemplate>
                                                  <asp:LinkButton ID="lUpdateRef" runat="server" Font-Bold="True" 
                                                      Font-Size="12px" ForeColor="White" onclick="lUpdateRef_Click" 
                                                      style="text-decaration:none;">Update</asp:LinkButton>
                                              </FooterTemplate>
                                              <ItemTemplate>
                                                  <asp:TextBox ID="txtgvDesig" runat="server" BackColor="Transparent" 
                                                      BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                      Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>' 
                                                      Width="150px"></asp:TextBox>
                                              </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Center" />
                                              <ItemStyle HorizontalAlign="Left" />
                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Phone">
                                              <ItemTemplate>
                                                  <asp:TextBox ID="txtgvPhone" runat="server" BackColor="Transparent" 
                                                      BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                      Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>' 
                                                      Width="100px"></asp:TextBox>
                                              </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Center" />
                                              <ItemStyle HorizontalAlign="Left" />
                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Mobile">
                                              <ItemTemplate>
                                                  <asp:TextBox ID="txtgvMobile" runat="server" BackColor="Transparent" 
                                                      BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                      Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>' 
                                                      Width="100px"></asp:TextBox>
                                              </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Center" />
                                              <ItemStyle HorizontalAlign="Left" />
                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Type" Visible="False">
                                              <ItemTemplate>
                                                  <asp:Label ID="lgvgval" runat="server" 
                                                      Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                      </Columns>
                                      <FooterStyle BackColor="#333333" />
                                      <PagerStyle HorizontalAlign="Center" />
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

