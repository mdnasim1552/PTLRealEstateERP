<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpRecruitment.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.EmpRecruitment" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

    .style24
    {
        width: 7px;
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
    
        .style48
        {
            width: 75px;
        }
        .style49
        {
            width: 39px;
        }
        .style50
        {
            width: 4px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table style="width: 99%;">
        <tr>
            <td class="style12">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="STUDENT PERSONAL  INFORMATION VIEW/EDIT" Width="500px"
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
                        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" BorderColor="Yellow" BorderWidth="1px">
                            <table style="width:100%; margin-bottom: 0px;">
                             <tr>
                                    <td class="style24">
                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Date:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style48">
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="txtboxformat" Width="110px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style50">
                                        &nbsp;</td>
                                    <td class="style49">
                                        &nbsp;</td>
                                    <td class="style36">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="button" Font-Bold="True" 
                                            Font-Size="12px" Font-Underline="False" Height="16px" onclick="lbtnOk_Click" 
                                            style="text-align: center; color: #FFFFFF; " Width="33px">Ok</asp:LinkButton>
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
                                </tr>
                                <tr>
                                    <td class="style24">
                                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Recruitment No.:" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style48">
                                        <asp:Label ID="lblRef" runat="server" BackColor="White" Width="115px"></asp:Label>
                                    </td>
                                    <td class="style50">
                                        &nbsp;</td>
                                    <td class="style49">
                                        &nbsp;</td>
                                    <td class="style36">
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White"></asp:Label>
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
                                </tr>
                                <tr>
                                    <td class="style24">
                                        <asp:Label ID="lblPre" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Pre. Recru." 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style48">
                                        <asp:TextBox ID="txtPreSer" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" TabIndex="4" 
                                            Width="110px"></asp:TextBox>
                                    </td>
                                    <td class="style50">
                                        <asp:ImageButton ID="ImgbtnFindPre" runat="server" Height="19px" 
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindPre_Click" 
                                            Style="margin-left: 0px" TabIndex="5" Width="21px" />
                                    </td>
                                    <td class="style49">
                                        <asp:DropDownList ID="ddlPreInfo" runat="server" Width="250px" 
                                            AutoPostBack="True" onselectedindexchanged="ddlPreInfo_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style36">
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
                    <td colspan="12">
                       
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="831px" 
                                                onrowdatabound="gvPersonalInfo_RowDataBound" ForeColor="Black">
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
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>' 
                                                                Width="537px"></asp:TextBox>
                                                            <asp:Panel ID="Panegrd" runat="server" BorderColor="Blue" BorderStyle="Solid" 
                                                                BorderWidth="1px" Height="25px" Width="543px">
                                                                <table style="width:92%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtgrdEmpSrc" runat="server" CssClass="txtboxformat" 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="style55">
                                                                            <asp:ImageButton ID="ibtngrdEmpList" runat="server" Height="18px" 
                                                                                ImageUrl="~/Image/find_images.jpg" onclick="ibtngrdEmpList_Click" 
                                                                                style="width: 18px" />
                                                                        </td>
                                                                        <td class="style56">
                                                                            <asp:DropDownList ID="ddlval" runat="server" AutoPostBack="True" 
                                                                                BackColor="#CAE4FF" onselectedindexchanged="ddlval_SelectedIndexChanged" 
                                                                                Width="370px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="style57">
                                                                            &nbsp;</td>
                                                                        <td class="style58">
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
                                                            </asp:Panel>
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

