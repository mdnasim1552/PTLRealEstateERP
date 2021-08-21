<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpEntryACR.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_91_ACR.EmpEntryACR" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/KeyPress.js" type="text/javascript"></script>
    
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
    
        .style39
        {
            width: 244px;
        }
        .style40
        {
            width: 76px;
        }
        .style41
        {
            width: 46px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table style="width: 99%;">
        <tr>
            <td class="style12">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="EMPLOYEE ACR INFORMATION VIEW/EDIT" Width="500px"
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
         <script src="../../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
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
                var gridview = $('#<%=this.gvACREntry.ClientID %>');
                $.keynavigation(gridview);

            };
        </script>
            <table style="width:100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel2" runat="server"  BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                         
                            <table style=" height: 41px;">
                                
                            
                       
                                
                                <tr>
                                    <td class="style41">
                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Company:" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style40">
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="txtboxformat" 
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnCompany" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnCompany_Click" 
                                            Width="16px" TabIndex="1" />
                                    </td>
                                    <td class="style39" colspan="2">
                                        <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="12px" Width="250px" 
                                            onselectedindexchanged="ddlCompany_SelectedIndexChanged" TabIndex="3">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>
                                  <tr>
                                    <td class="style41">
                                        <asp:Label ID="lbSection" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Section:" 
                                            Width="90px"></asp:Label>
                                    </td>
                                    <td class="style40">
                                        <asp:TextBox ID="txtSection" runat="server" CssClass="txtboxformat" 
                                            Width="100px" TabIndex="4"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgSecSearch" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgSecSearch_Click" 
                                            style="width: 18px" TabIndex="5" />
                                    </td>
                                    <td class="style39" colspan="2">
                                        <asp:DropDownList ID="ddlSectionName" runat="server" 
                                            Font-Bold="True" Font-Size="12px" Width="250px" AutoPostBack="True" 
                                            onselectedindexchanged="ddlSectionName_SelectedIndexChanged" TabIndex="6">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblSec" runat="server" BackColor="White" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="Blue" 
                                            Width="250px"></asp:Label>

                                    </td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style41">
                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Employee List:" 
                                            Width="90px"></asp:Label>
                                    </td>
                                    <td class="style40">
                                        <asp:TextBox ID="txtEmpSrc" runat="server" CssClass="txtboxformat" 
                                            Width="100px" TabIndex="7"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnEmpList" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnEmpList_Click" 
                                            style="width: 18px" TabIndex="8" />
                                    </td>
                                    <td class="style39" colspan="2">
                                        <asp:DropDownList ID="ddlEmpName" runat="server" 
                                            Font-Bold="True" Font-Size="12px" Width="250px" AutoPostBack="True" 
                                            onselectedindexchanged="ddlEmpName_SelectedIndexChanged" TabIndex="9">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblEmp" runat="server" BackColor="White" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="Blue" 
                                            Width="250px"></asp:Label>
                                    </td>
                                    <td class="style36">
                                        <asp:Label ID="lbltxtTotalSal" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="text-align: right; color: #FFFFFF;" Text="Net Salary:" Visible="False" 
                                            Width="126px"></asp:Label>
                                        <asp:Label ID="lbltotalsal" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#660033" style="color: #FFFFCC" Width="120px"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style41">
                                        <asp:Label ID="lbdesg0" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text=" Designation:" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style40">
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="txtboxformat" Width="100px" 
                                            TabIndex="10"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton2" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" Width="16px" 
                                            onclick="ImageButton2_Click" TabIndex="11" />
                                    </td>
                                    <td class="style39" colspan="2">
                                        <asp:Label ID="lblDesignation" runat="server" BackColor="White" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="Blue" 
                                            Width="250px"></asp:Label>
                                    </td>
                                    <td class="style36">
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style41">
                                        <asp:Label ID="lbPrAcr" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Previous ACR" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style40">
                                        <asp:TextBox ID="txtPreNum" runat="server" CssClass="txtboxformat" 
                                            Width="100px" TabIndex="12"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgBtnPreAcr" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" 
                                            Width="16px" onclick="imgBtnPreAcr_Click" TabIndex="13" />
                                    </td>
                                    <td class="style39" colspan="2">
                                        <asp:DropDownList ID="ddlPrevISSList" runat="server" Font-Size="12px" 
                                            Width="250px" TabIndex="14">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="style17">
                                        <asp:Label ID="Label7" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" Height="16px" style="TEXT-ALIGN: right" 
                                            Text="ACR Date:" Width="90px" ForeColor="White"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtCurTransDate" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" 
                                            Format="dd.MM.yyyy" TargetControlID="txtCurTransDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style23">
                                        <asp:Label ID="Label15" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Height="16px" style="TEXT-ALIGN: right" 
                                            Text="ACR No:" Width="130px"></asp:Label>
                                    </td>
                                    <td class="style23">
                                        <asp:Label ID="lblCurTransNo1" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: right; background-color: #FFFFFF;" 
                                            Width="45px"></asp:Label>
                                        <asp:TextBox ID="txtCurTransNo2" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ReadOnly="True" 
                                            Width="55px">00001</asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:LinkButton ID="infoOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="infoOk_Click" Width="30px" 
                                            style="text-align: center; height: 17px;" TabIndex="15" >Ok</asp:LinkButton>
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
                                    <td class="style35">
                                        </td>
                                    <td class="style34">
                                        </td>
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
                                            <asp:GridView ID="gvACREntry" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="831px">
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
                                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "gdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim(): "")  %>' 
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
                                                    <asp:TemplateField HeaderText="Standard">
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lUpdatACRInfo" runat="server" Font-Bold="True" 
                                                                Font-Size="12px" ForeColor="White" onclick="lUpdatACRInfo_Click" 
                                                                style="text-decaration:none;">Update ACR Info</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                          
                                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" TabIndex="15"
                                                                BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>' 
                                                                Width="250px"></asp:TextBox>
                                                            
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Evaluation 1">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvEv1" runat="server" BackColor="Transparent" TabIndex="16"
                                                                BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "evl1")) %>' 
                                                                Width="100px"></asp:TextBox>
                                                            
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Evaluation 2">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvEv2" runat="server" BackColor="Transparent" TabIndex="17"
                                                                BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "evl2")) %>' 
                                                                Width="100px"></asp:TextBox>
                                                            
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Evaluation 3">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvEv3" runat="server" BackColor="Transparent" TabIndex="18"
                                                                BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "evl3")) %>' 
                                                                Width="100px"></asp:TextBox>
                                                            
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

