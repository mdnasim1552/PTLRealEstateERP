<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptEmpIncrAPro.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.RptEmpIncrAPro" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

    .style16
    {
        width: 9px;
    }
    .style15
    {
        width: 44px;
    }
        .style44
        {
            width: 96px;
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
    font-size: 12px;
	    font-weight: normal;
	margin-right: 0px;
   text-align: left;
}
    .style24
    {
        width: 10px;
    }
        .style43
        {
            width: 116px;
        }
        .style38
        {
            width: 78px;
        }
        .style41
        {
            width: 121px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

   
   <script language="javascript" type="text/javascript">
       $(document).ready(function () {
           //For navigating using left and right arrow of the keyboard
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
           $('#<%=this.txtSrcCompany.ClientID %>').focus();

       });
       function pageLoaded() {

           $("input, select").bind("keydown", function (event) {
               var k1 = new KeyPress();
               k1.textBoxHandler(event);
           });
          
       };
   </script>
  
 
 <br />
 
 <table style="width: 100%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblHtitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="EMPLOYEE SALARY INFORMATION" Width="667px"
                   STYLE="border-bottom:1px solid WHITE;border-top:1px solid WHITE;" 
                    Height="16px" ></asp:Label>
            </td>
            <td class="style33">
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" Font-Size="12px" 
                    onclick="lbtnPrint_Click" CssClass="button" ForeColor="White">PRINT</asp:LinkButton>
                                                </td>
        </tr>
        </table>
        
                
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                    <td>
                        <asp:Panel ID="Panel4" runat="server" BorderColor="black" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style16">
                                        &nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="black" style="text-align: left" Text="Company:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="txtboxformat" 
                                            Width="100px" TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:ImageButton ID="imgbtnCompany" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnCompany_Click" TabIndex="1" 
                                            Width="16px" />
                                    </td>
                                    <td align="left" colspan="7">
                                        <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="12px" 
                                            onselectedindexchanged="ddlCompany_SelectedIndexChanged" TabIndex="2" 
                                            Width="300px">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" BackColor="#003366" 
                                            BorderColor="black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Height="16px" onclick="lnkbtnShow_Click" 
                                            style="text-align: center; " Width="50px" TabIndex="12">Ok</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="black" 
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="Yellow" style="text-align: left" Text="Please wait . . . . . . ." 
                                                    Width="120px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
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
                                    <td class="style16">
                                        &nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="black" style="text-align: left" Text="Department:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" TabIndex="6" 
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:ImageButton ID="imgbtnProSrch" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnProSrch_Click" TabIndex="7" 
                                            Width="16px" />
                                    </td>
                                    <td align="left" colspan="7">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="12px" 
                                            onselectedindexchanged="ddlDepartment_SelectedIndexChanged" TabIndex="8" 
                                            Width="300px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlDepartment_ListSearchExtender" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlDepartment">
                                        </cc1:ListSearchExtender>
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
                                </tr>
                                <tr>
                                    <td class="style16">
                                        </td>
                                    <td class="style15">
                                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="black" style="text-align: left" Text="Section:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtSrcSec" runat="server" CssClass="txtboxformat" TabIndex="9" 
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:ImageButton ID="imgbtnSecSrch" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnSecSrch_Click" TabIndex="10" 
                                            Width="16px" />
                                    </td>
                                    <td colspan="7">
                                
                                           <asp:DropDownList ID="ddlSection" runat="server" 
                                               Font-Bold="True" Font-Size="12px" TabIndex="8" 
                                               Width="300px">
                                           </asp:DropDownList>
                                          
            
            
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
                                    <td class="style16">
                                        &nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="lblfrmdate" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="black"  Text="From:" Width="80px"></asp:Label>
                                    </td>
                                    <td align="left" class="style44">
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="txtboxformat" 
                                            Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style24">
                                        <asp:Label ID="lbltodate" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="black" style="text-align: right" Text="To:"></asp:Label>
                                    </td>
                                    <td class="style43">
                                        <asp:TextBox ID="txttodate" runat="server" BorderStyle="None" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td align="left" class="style38">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            Height="16px" style="color: #FFFFFF; text-align: left;" Text="Page Size:" 
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td class="style41">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" 
                                            style="margin-left: 0px" TabIndex="11" Width="100px">
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
                                    <td class="style41">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White"></asp:Label>
                                    </td>
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
            </table>

            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <asp:GridView ID="gvEmpinc" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" ShowFooter="True" Width="715px" PageSize="15" 
                        onpageindexchanging="gvEmpinc_PageIndexChanging">
                        <RowStyle BackColor="#D2FFF7" Font-Size="12px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No."><ItemTemplate><asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" 
                                        style="text-align: right" 
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                              <asp:TemplateField HeaderText="Department"><ItemTemplate><asp:Label ID="lblgvdept" runat="server" Font-Bold="true" Font-Size="11px" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>' 
                                        Width="120px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>

                            <asp:TemplateField HeaderText="Section"><ItemTemplate><asp:Label ID="lblgvSection" runat="server" Font-Size="11px" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>' 
                                        Width="120px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFTotal" runat="server" Font-Bold="True" 
                                        Font-Size="12px" ForeColor="White" style="text-align: left" Width="80px" Text="Total"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate><asp:Label ID="lblgvEmpName" runat="server"
                                                           Text='<%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>' 
                                                           Width="150px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                            <asp:TemplateField HeaderText="Card #"><ItemTemplate><asp:Label ID="lblgvCardno" runat="server" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>' 
                                        Width="60px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>

                               <asp:TemplateField HeaderText="Joining Date"><ItemTemplate><asp:Label ID="lblgvjdate" runat="server" 
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>' 
                                        Width="70px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Present Salary"><ItemTemplate><asp:Label ID="lblgvpresalary" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "presal")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="80px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lblgvFpresalary" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="White" style="text-align: right" Width="80px"></asp:Label></FooterTemplate><ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                                               
                             <asp:TemplateField HeaderText="Grade "><ItemTemplate><asp:Label ID="lblgvgrade" runat="server" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grade")) %>' 
                                        Width="80px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>

                            <asp:TemplateField HeaderText="Increment %"><ItemTemplate><asp:Label ID="lblgvinpercnt" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inpercnt")).ToString("#,##0.00;(#,##0.00); ")+"%" %>' 
                                        Width="70px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>



                            <asp:TemplateField HeaderText="Increment  Amt."><ItemTemplate><asp:Label ID="lblgvincam" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incam")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="60px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lblgvFgvincam" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="White" style="text-align: right" Width="80px"></asp:Label></FooterTemplate><ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="Right" /></asp:TemplateField>
                            <asp:TemplateField HeaderText="Salary After Increment."><FooterTemplate><asp:Label ID="lblgvFsalaincam" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="White" style="text-align: right" Width="80px"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblgvsalaincam" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salaincmnt")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="80px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="Right" /></asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#333333" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                            ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                    </asp:GridView>
                </asp:View>
            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

