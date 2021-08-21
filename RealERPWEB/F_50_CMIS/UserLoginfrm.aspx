<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="UserLoginfrm.aspx.cs" Inherits="RealERPWEB.F_50_CMIS.UserLoginfrm" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style17
        {
            width: 190px;
        }
        .style18
        {
            width: 88px;
        }
        .style19
        {
            width: 45px;
        }
        .style20
        {
            width: 204px;
        }
        .style21
        {
            width: 321px;
        }
        .style22
        {
            width: 458px;
        }
        .style23
        {
            width: 86px;
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
        .style24
        {
            width: 83px;
        }
        .style25
        {
            width: 4px;
        }
        .style26
        {
            width: 82px;
        }
    </style>
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
  <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
  
 <script language="javascript" type="text/javascript">

     $(document).ready(function () {
         Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

     });
     function pageLoaded() {


         $("input, select").bind("keydown", function (event) {
             var k1 = new KeyPress();
             k1.textBoxHandler(event);

         });



         var gv = $('#<%=this.gvPermission.ClientID %>');
         gv.Scrollable();
     }
 </script>
    
    <table style="width: 97%;">
        <tr>
            <td class="style34">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="USER LOGIN FORM " Width="575px"
                   STYLE="border-bottom:1px solid #CC3399;border-top:1px solid #CC3399;" ></asp:Label>
            </td>
            <td class="style32">
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style33">
                                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList></td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" Font-Size="12px" 
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
            <td>
                <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                    BorderWidth="1px">
                    <table style="width:100%;">
                        <tr>
                            <td class="style24">
                                <asp:TextBox ID="txtSrcName" runat="server" BorderStyle="None" 
                                    CssClass="txtboxformat" Width="80px"></asp:TextBox>
                            </td>
                            <td class="style25">
                                <asp:ImageButton ID="ibtnFindName" runat="server" Height="18px" 
                                    ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindName_Click" 
                                    style="width: 18px" TabIndex="1" />
                            </td>
                            <td class="style26">
                              <asp:Label ID="lblId" runat="server" BackColor="White" Font-Bold="True" 
                                    ForeColor="Black" Text="User Name:" Visible="False" Width="90px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txtuserid" runat="server" BackColor="White" Font-Bold="True" 
                                    ForeColor="Black" Visible="False" Width="300px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMsg" runat="server" BackColor="Red" Font-Bold="True" 
                                    Font-Size="12px" ForeColor="White"></asp:Label>
                            </td>
                            <td>
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
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                                    <asp:GridView ID="gvUseForm" runat="server" AutoGenerateColumns="False" 
                                        ShowFooter="True" Width="918px" AllowPaging="True" 
                                        onpageindexchanging="gvUseForm_PageIndexChanging" 
                                        onrowcancelingedit="gvUseForm_RowCancelingEdit" 
                                        onrowediting="gvUseForm_RowEditing" onrowupdating="gvUseForm_RowUpdating" 
                                        PageSize="100">
                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:CommandField CancelText="Can" ShowEditButton="True" UpdateText="Up" />
                                            <asp:TemplateField HeaderText="User Id">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnUserId" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>' 
                                                        Width="50px" onclick="lbtnUserId_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                                
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvuserid" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" MaxLength="7" Width="50px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Short Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvusrShorName" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>' 
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtusrShorName" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None"  Width="120px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Full Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvusrFullName" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>' 
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtusrFullName" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Width="120px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDesig" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>' 
                                                        Width="120px"></asp:Label>
                                                         </ItemTemplate>
                                                        <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvDesig" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Width="120px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'></asp:TextBox>
                                                </EditItemTemplate>
                                               
                                                
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pass Word">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvpass" runat="server" BackColor="Transparent"  
                                                        BorderStyle="None" Width="140px"                                                         
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrpass")) %>' TextMode="Password" 
                                                        ></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkActive" runat="server" 
                                                        
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usractive"))=="True" %>' 
                                                         />
                                                </ItemTemplate>
                                                  <%--<EditItemTemplate>
                                                    
                                                </EditItemTemplate>--%>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrmrk" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>' 
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                  <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvgvrmrk" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Width="120px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                                                                      
                                        </Columns>
                                        <FooterStyle BackColor="#333333" />
                                        <PagerStyle HorizontalAlign="Left" ForeColor="White" />
                                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                    </asp:GridView>
                                </td>
        </tr>
        <tr>
            <td>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td colspan="12">
                                    <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                        BorderWidth="1px">
                                        <table style="width:900px;">
                                            <tr>
                                                <td class="style18">
                                                    <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="color: #FFFFFF; text-align: left;" Text="Page Size:" Visible="False" 
                                                        Width="80px"></asp:Label>
                                                </td>
                                                <td class="style19">
                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                        BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                        onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Visible="False" 
                                                        Width="70px">
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                        <asp:ListItem Value="100">100</asp:ListItem>
                                                        <asp:ListItem Value="150">150</asp:ListItem>
                                                        <asp:ListItem Value="200">200</asp:ListItem>
                                                        <asp:ListItem Value="300">300</asp:ListItem>
                                                        <asp:ListItem Value="400">400</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkShowall" runat="server" AutoPostBack="True" 
                                                        Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                        oncheckedchanged="chkShowall_CheckedChanged" Text="Show All" Width="70px" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblModNmae" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="color: #FFFFFF; text-align: right;" Text="Module Name:" Width="80px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlModName" runat="server" AutoPostBack="True" 
                                                        Font-Bold="True" Font-Size="12px" 
                                                        onselectedindexchanged="ddlModName_SelectedIndexChanged" Width="210px">  
                                                         <asp:ListItem Value="00">All</asp:ListItem>                                                       
                                                        <asp:ListItem  Value="50">&nbsp; A.&nbsp;Consolidate MIS</asp:ListItem> 



                                                       



                                                              
                                                      
                                                      
                                                      
                                                      
                                                        <%--<asp:ListItem Value="01">Budget</asp:ListItem>
                                                        <asp:ListItem Value="02">Project Implementaion</asp:ListItem>
                                                        <asp:ListItem Value="03">Procurement</asp:ListItem>
                                                        <asp:ListItem Value="04">Accounts</asp:ListItem>
                                                        <asp:ListItem Value="05">Marketing</asp:ListItem>
                                                        <asp:ListItem Value="06">Sales &amp; Recovery</asp:ListItem>
                                                        <asp:ListItem Value="07">Credit Realization(CR)</asp:ListItem>
                                                        <asp:ListItem Value="08">Management</asp:ListItem>
                                                        <asp:ListItem Value="09">Feasibility</asp:ListItem>
                                                        <asp:ListItem Value="10">MIS</asp:ListItem>
                                                        <asp:ListItem Value="11">Fixed Assets</asp:ListItem>
                                                        <asp:ListItem Value="12">Billing</asp:ListItem>
                                                        <asp:ListItem Value="13">Tender</asp:ListItem>
                                                        <asp:ListItem  Value="14">Tender Management</asp:ListItem>
                                                        <asp:ListItem  Value="17">Land Proposal</asp:ListItem>
                                                        <asp:ListItem  Value="18">Finance Module</asp:ListItem>
                                                         <asp:ListItem Value="31"> MIS Module</asp:ListItem> 
                                                        <asp:ListItem Value="32"> Management Module</asp:ListItem>  
                                                        <asp:ListItem Value="99">Admin</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style21">
                                                    &nbsp;</td>
                                                <td class="style21">
                                                    <asp:Label ID="lblMsg1" runat="server" BackColor="Red" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White"></asp:Label>
                                                </td>
                                                <td class="style20">
                                                    <asp:LinkButton ID="lnkbtnBack" runat="server" BackColor="#003366" 
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="18px" ForeColor="White" onclick="lnkbtnBack_Click">Back</asp:LinkButton>
                                                </td>
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
                                    <asp:GridView ID="gvPermission" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" 
                                        onpageindexchanging="gvPermission_PageIndexChanging" 
                                        onrowdeleting="gvPermission_RowDeleting" ShowFooter="True">
                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Form Name" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvufrmname" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmname")) %>' 
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Form id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvufrmid" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmid")) %>' 
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Query Type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvQrytype" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qrytype")) %>' 
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDescription" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "dscrption").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "dscrption")).Trim(): "") 
                                                                         
                                                                    %>' Width="300px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnUpPer" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" onclick="lbtnUpPer_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderTemplate>
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td class="style22">
                                                                Description</td>
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
                                                            <td class="style23">
                                                                &nbsp;</td>
                                                            <td>
                                                                <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True" 
                                                                    oncheckedchanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View/Show">
                                                <HeaderTemplate>
                                                    <table style="width:90px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="View/Show"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkallView" runat="server" AutoPostBack="True" 
                                                                    oncheckedchanged="chkallView_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPermit" runat="server" 
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkper"))=="True" %>' 
                                                        Width="50px" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry/Edit">
                                                <HeaderTemplate>
                                                    <table style="width:90px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text="View/Show"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkallEntry" runat="server" AutoPostBack="True" 
                                                                    oncheckedchanged="chkallEntry_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEntry" runat="server" 
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entry"))=="True" %>' 
                                                        Width="50px" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Print">
                                                <HeaderTemplate>
                                                    <table style="width:90px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text="Print"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkallPrint" runat="server" AutoPostBack="True" 
                                                                    oncheckedchanged="chkallPrint_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPrint" runat="server" 
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "printable"))=="True" %>' 
                                                        Width="50px" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Check All">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="chkall_CheckedChanged" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#333333" />
                                        <PagerStyle ForeColor="White" HorizontalAlign="Left" />
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
                                <td class="style17">
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblusrid" runat="server" Visible="False"></asp:Label>
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

