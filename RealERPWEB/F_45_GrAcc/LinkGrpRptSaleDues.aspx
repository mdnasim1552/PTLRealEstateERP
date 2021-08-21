
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpRptSaleDues.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpRptSaleDues" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .style12
        {
            width: 102px;
        }
        .style21
        {
            width: 216px;
        }
        .StyleCheckBoxList
        {
            text-transform: capitalize;
            margin-bottom: 0px;
        }
        
                
      
        .style40
        {
            width: 86px;
            height: 23px;
        }
        .style41
        {
            width: 57px;
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
        .style42
        {
            width: 18px;
        }
        .style43
        {
            width: 287px;
            height: 23px;
        }
        
      
        .style39
        {
            height: 23px;
        }
        
      
        .style38
        {
            width: 57px;
            height: 23px;
        }
                
      
        .style36
        {
            height: 23px;
            width: 18px;
        }
        
      
        .style27
        {
            color: #FFFFFF;
        }
        
              
        .style23
        {
            height: 23px;
        }
        .style55
        {
            width: 53px;
            height: 23px;
        }
        .style56
        {
            width: 148px;
            height: 23px;
        }
        .style57
        {
            width: 58px;
            height: 23px;
        }
        .style58
        {
        }
        

    .style50
    {
        color: white;
    }
        </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 98%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="RECEIVED LIST INFORMATION" Width="600px"
                   STYLE="border-bottom:1px solid blue;border-top:1px solid blue;" ></asp:Label>
            </td>
            <td class="style21">
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
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" Font-Size="12px" 
                    onclick="lbtnPrint_Click" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
                
                
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                        <table style="width:100%;">
                            <tr>
                                <td>
                                
                                    <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                        BorderWidth="1px" Width="856px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style40">
                                                    <asp:Label ID="Label5" runat="server" CssClass="style50" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="Project Name:" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style38">
                                                    <asp:Label ID="lblProjectName" runat="server" BackColor="#000066" 
                                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Yellow" Width="300px"></asp:Label>
                                                </td>
                                                <td class="style36">
                                                </td>
                                                <td class="style43" colspan="4">
                                                </td>
                                                <td class="style39">
                                                    </td>
                                                <td class="style39">
                                                </td>
                                                <td class="style39">
                                                </td>
                                                <td class="style39">
                                                </td>
                                                <td class="style39">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style39">
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="text-align: left; color: #FFFFFF;" Text="Date:" Width="90px"></asp:Label>
                                                </td>
                                                <td class="style38">
                                                    <asp:Label ID="lbldaterange" runat="server" BackColor="#000066" 
                                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Yellow" Width="300px"></asp:Label>
                                                </td>
                                                <td class="style36">
                                                    <asp:Label ID="lblPage" runat="server" CssClass="style27" Font-Bold="True" 
                                                        Font-Size="12px" Font-Underline="False" ForeColor="White" Height="16px" 
                                                        style="font-weight: 700; text-align: right" Text="Size :" Width="32px"></asp:Label>
                                                </td>
                                                <td align="left" class="style56">
                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                        BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                        onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Width="80px">
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
                                                <td align="left" class="style57">
                                                    &nbsp;</td>
                                                <td align="left" class="style55">
                                                    &nbsp;</td>
                                                <td align="left" class="style56">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            
                                <tr>
                                    <td class="style22">
                                        <asp:MultiView ID="MultiView1" runat="server">
                                            <asp:View ID="ViewRecList02" runat="server">

                                                  <table style="width: 100%;">
                                                      <tr>
                                                          <td colspan="12">
                                                             
                                                              <asp:GridView ID="dgvAccRec02" runat="server" AllowPaging="True" 
                                                                  AutoGenerateColumns="False" onpageindexchanging="dgvAccRec02_PageIndexChanging" 
                                                                  ShowFooter="True" style="text-align: left" Width="654px" 
                                                                  onrowdatabound="dgvAccRec02_RowDataBound">
                                                                  <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                                  <Columns>
                                                                      <asp:TemplateField HeaderText="Sl.No.">
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lblgvSlNo0" runat="server" 
                                                                                  Font-Bold="True" Height="16px" 
                                                                                  style="text-align: right" 
                                                                                  Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                                                                     

                                                                       <asp:TemplateField HeaderText="Usircode" Visible="false">
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgusircode" runat="server" 
                                                                                  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>' 
                                                                                  Width="140px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                                                     
                                                                      <asp:TemplateField HeaderText="Project Name">
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgactdesc02" runat="server" 
                                                                                  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                                                  Width="140px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Cutomer Name">
                                                                          <ItemTemplate>
                                                                              <asp:LinkButton ID="lbtngacuname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))
                                                                       %>' Width="150px"  
                                                                                  Style=" color:Black; text-align: left; font-weight:bold; text-decoration:none;" 
                                                                                  onclick="lbtngacuname_Click" ></asp:LinkButton></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Left" />
                                                                          
                                                                          
                                                                          
                                                                          </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Unit Desc.">
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgudesc01" runat="server" 
                                                                                  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>' 
                                                                                  Width="90px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Flat Size">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFunitsize" runat="server" 
                                                                                  Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvunitsize" runat="server" style="text-align: right" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="60px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" />
                                                                          <FooterStyle HorizontalAlign="right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Rate">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFavgrate" runat="server" 
                                                                                  Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvrate" runat="server" style="text-align: right" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptrate")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="60px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" />
                                                                          <FooterStyle HorizontalAlign="right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Apartment Price">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFaptcost" runat="server" 
                                                                                  Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvaptcost" runat="server" style="text-align: right" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptcost")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" />
                                                                          <FooterStyle HorizontalAlign="right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Car Parking">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFcpcost" runat="server" 
                                                                                  Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvcpcost" runat="server" style="text-align: right" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpcost")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" />
                                                                          <FooterStyle HorizontalAlign="right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Utility ">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFutilitycost" 
                                                                                  runat="server" Font-Bold="True" 
                                                                                  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvutilitycost" runat="server" style="text-align: right" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utltycost")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" />
                                                                          <FooterStyle HorizontalAlign="right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Others">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFothcost" runat="server" 
                                                                                  Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvothescost" runat="server" style="text-align: right" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othcost")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" />
                                                                          <FooterStyle HorizontalAlign="right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Total Value">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFtocost" runat="server" 
                                                                                  Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvtocsot" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>

                                                <asp:TemplateField HeaderText="Encash">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgFEncash" runat="server" 
                                                            ForeColor="White"></asp:Label></FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvEncash" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="60px"></asp:Label></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" 
                                                        Font-Size="11px" Font-Bold="True" /></asp:TemplateField>
                                        

                                                 <asp:TemplateField HeaderText="Returned">
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvFtretamt" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvtretamt" runat="server" Font-Size="11PX" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retcheque")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label></ItemTemplate>
                                                     <HeaderStyle HorizontalAlign="Center" />
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     <FooterStyle HorizontalAlign="right" /></asp:TemplateField>


                                      <asp:TemplateField HeaderText="Today's">
                                          <FooterTemplate>
                                              <asp:Label ID="lgvFtframt" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="lgvtframt" runat="server" Font-Size="11PX" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcheque")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label></ItemTemplate>
                                          <HeaderStyle HorizontalAlign="Center" />
                                          <ItemStyle HorizontalAlign="Right" />
                                          <FooterStyle HorizontalAlign="right" /></asp:TemplateField>



                                              <asp:TemplateField HeaderText="Post Dated">
                                                  <FooterTemplate>
                                                      <asp:Label ID="lgvFtpdamt" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                  <ItemTemplate>
                                                      <asp:Label ID="lgvtpdamt" runat="server" Font-Size="11PX" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcheque")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label></ItemTemplate>
                                                  <HeaderStyle HorizontalAlign="Center" />
                                                  <ItemStyle HorizontalAlign="Right" />
                                                  <FooterStyle HorizontalAlign="right" /></asp:TemplateField>
                                 












                                                                      <asp:TemplateField HeaderText="Total Received">
                                                                          <FooterTemplate>
                                                                              <asp:HyperLink ID="hlnkgvFtoreceived" 
                                                                                  runat="server"  Font-Bold="True" Font-Size="12px" ForeColor="White" Target="_blank"
                                                                            Style="text-align: right" Width="100px"></asp:HyperLink></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvtotreceived" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Total Dues">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFatodues" runat="server" 
                                                                                  Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvatodues" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atodues")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Dues Upto Dec">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFtotaldues" 
                                                                                  runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvtotaldues" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todues")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Dues Balance">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFtodues" runat="server" 
                                                                                  Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvtodues" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Previous Booking">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFpbooking" runat="server" 
                                                                                  Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvtodues0" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbookam")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Previous Installment">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFpinstallment" 
                                                                                  runat="server" Font-Bold="True" 
                                                                                  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvtodues1" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pinsam")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Total Amt">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFpretodues" 
                                                                                  runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvtodues2" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ptodues")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Current Booking ">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFCbooking" runat="server" 
                                                                                  Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvCbooking" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbookam")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Current Installment ">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFCinstallment" 
                                                                                  runat="server" Font-Bold="True" 
                                                                                  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvCinstallment" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cinsam")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Total ">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFtoCInstalment" 
                                                                                  runat="server" Font-Bold="True" 
                                                                                  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvCoCInstalment" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ctodues")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                                      
                                                                      
                                                                      
                                                                      <asp:TemplateField HeaderText="Total Due">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFvtodues" runat="server" 
                                                                                  Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>

                                                                           


                                                                              <asp:Label ID="lblgvvtodues" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vtodues")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"  
                                                                                  BorderStyle="None" Font-Size="11px"  ></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>


                                                                      <asp:TemplateField HeaderText="Delay Charge">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFdelcharge" 
                                                                                  runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lblgvdelcharge" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdelay")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"
                                                                                  BorderStyle="None" Font-Size="12px" ></asp:Label></ItemTemplate>
                                                                          
                                                                          
                                                                          
                                                                          
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                          <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>



                                                                      <asp:TemplateField HeaderText="Return Cheque">
                                                                          <FooterTemplate>
                                                                              <asp:Label ID="lgvFdischarge" 
                                                                                  runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                          <ItemTemplate>
                                                                              <asp:Label ID="lgvdischarge" runat="server" 
                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discharge")).ToString("#,##0;(#,##0); ") %>' 
                                                                                  Width="70px"></asp:Label></ItemTemplate>
                                                                          <HeaderStyle HorizontalAlign="Center" />
                                                                                  <ItemStyle HorizontalAlign="Right" /></asp:TemplateField>

                                                                   <asp:TemplateField HeaderText="Net Total Dues">
                                                                       <FooterTemplate>
                                                                           <asp:Label ID="lgvFnettodues" 
                                                                               runat="server" Font-Bold="True" Font-Size="12px" 
                            ForeColor="White" style="text-align: right"></asp:Label></FooterTemplate>
                                                                       <ItemTemplate>
                                                                           <asp:LinkButton ID="lbtngvnettodues" runat="server" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ntodues")).ToString("#,##0;(#,##0); ") %>' 
                            Width="70px" Style=" color:Black; text-align: right; font-weight:bold; text-decoration:none;" 
                                                                               onclick="lbtngvnettodues_Click" ></asp:LinkButton></ItemTemplate><HeaderStyle HorizontalAlign="Center" /><ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                                                                  </Columns>
                                                                  <FooterStyle BackColor="#333333" HorizontalAlign="Right" />
                                                                  <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="left" />
                                                                  <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                                      ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                                  <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                                  <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                                              </asp:GridView>
                                                          </td>
                                                      </tr>
                                                      <tr>
                                                          <td colspan="12">
                                                            
                                                              <asp:Panel ID="pnlIndPro" runat="server" Visible="False">
                                                                  <table style="width:100%;">
                                                                      <tr>
                                                                          <td class="style58">
                                                                              <asp:Label runat="server" BackColor="#000066" BorderColor="Yellow" 
                                                                                  BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                                                                  ForeColor="Yellow" Width="300px">Note</asp:Label>
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
                                                                          <td>
                                                                              &nbsp;</td>
                                                                          <td>
                                                                              &nbsp;</td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td class="style58" colspan="12">
                                                                              <asp:GridView ID="gvinpro" runat="server" AutoGenerateColumns="False" 
                                                                                  ShowFooter="True" Width="337px">
                                                                                  <PagerSettings Position="Top" />
                                                                                  <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                                                  <Columns>
                                                                                      <asp:TemplateField HeaderText="Sl.No.">
                                                                                          <ItemTemplate>
                                                                                              <asp:Label ID="serialnoid0" runat="server" 
                                                                                                  Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                                                          </ItemTemplate>
                                                                                        
                                                                                      </asp:TemplateField>
                                                                                 
                                                                                      
                                                                                      <asp:TemplateField HeaderText="Decription">
                                                                                          <ItemTemplate>
                                                                                              <asp:Label ID="lbldesinpro" runat="server" BackColor="Transparent" 
                                                                                                  BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black" 
                                                                                                  TabIndex="76" 
                                                                                                  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc")) %>' 
                                                                                                  Width="60px"></asp:Label>
                                                                                          </ItemTemplate>
                                                                                      </asp:TemplateField>
                                                                                      <asp:TemplateField HeaderText="Unit">
                                                                                          <ItemTemplate>
                                                                                              <asp:Label ID="lgvtounit" runat="server" BackColor="Transparent" 
                                                                                                  BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black" 
                                                                                                  TabIndex="76" 
                                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unumber")).ToString("#,##0;(#,##0); ") %>' 
                                                                                                  Width="80px"></asp:Label>
                                                                                                  <ItemStyle HorizontalAlign="Right" />
                                                                                          </ItemTemplate>
                                                                                      </asp:TemplateField>

                                                                                      <asp:TemplateField HeaderText="Total Unit Size">
                                                                                          <ItemTemplate>
                                                                                              <asp:Label ID="lgvtounsize" runat="server" BackColor="Transparent" 
                                                                                                  BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black" 
                                                                                                  TabIndex="76" 
                                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>' 
                                                                                                  Width="80px"></asp:Label>
                                                                                          </ItemTemplate>
                                                                                          <ItemStyle HorizontalAlign="Right" />
                                                                                      </asp:TemplateField>

                                                                                      <asp:TemplateField HeaderText="Rate">
                                                                                          <ItemTemplate>
                                                                                              <asp:Label ID="lgvtourate" runat="server" BackColor="Transparent" 
                                                                                                  BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black" 
                                                                                                  TabIndex="76" 
                                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>' 
                                                                                                  Width="80px"></asp:Label>
                                                                                          </ItemTemplate>
                                                                                          <ItemStyle HorizontalAlign="Right" />
                                                                                      </asp:TemplateField>
                                                                                      <asp:TemplateField HeaderText="Amount">
                                                                                          <ItemTemplate>
                                                                                              <asp:Label ID="lgvtouamt" runat="server" BackColor="Transparent" 
                                                                                                  BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black" 
                                                                                                  TabIndex="76" 
                                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>' 
                                                                                                  Width="80px"></asp:Label>
                                                                                          </ItemTemplate>
                                                                                          <ItemStyle HorizontalAlign="Right" />
                                                                                      </asp:TemplateField>


                                                                                      <asp:TemplateField HeaderText="Precentate">
                                                                                      <ItemStyle HorizontalAlign="Right" />
                                                                                          <ItemTemplate>
                                                                                              <asp:Label ID="lgvtoupercent" runat="server" BackColor="Transparent" 
                                                                                                  BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black" 
                                                                                                  TabIndex="76" 
                                                                                                  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                                                  Width="80px"></asp:Label>
                                                                                          </ItemTemplate>
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
                                                                  </table>
                                                              </asp:Panel>
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
                                            </asp:View>
                                        </asp:MultiView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style34">
                                       
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style198">
                                        <asp:Panel ID="PnlRmrks" runat="server" BorderColor="Maroon" 
                                            BorderStyle="Solid" BorderWidth="1px" Visible="False">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style210">
                                                        &nbsp;</td>
                                                    <td class="style214">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style212">
                                                        &nbsp;</td>
                                                    <td class="style213">
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
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style210">
                                                        &nbsp;</td>
                                                    <td align="left" class="style211" colspan="4">
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
                            </table>
                      
                            
                       
                   <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            
</asp:Content>






