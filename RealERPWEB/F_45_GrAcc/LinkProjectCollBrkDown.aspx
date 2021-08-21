<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkProjectCollBrkDown.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkProjectCollBrkDown" %>
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
        .style59
        {
            width: 28px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" language="javascript">
     $(document).ready(function () {
         Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


     });
     function pageLoaded() {

         var gv = $('#<%=this.gvIndPrjDet.ClientID %>');
         gv.Scrollable();



     }

 </script>

 <table style="width: 912px">
        <tr>
            <td class="style43">
                <asp:Label ID="lblHeadtitle" runat="server" BackColor="Blue" Font-Bold="True" 
                ForeColor="Yellow" style="font-weight: 700; color: #FFFF66; text-align: left" Text="PROJECT WISE COLLECTION BREAK DOWN REPORT" 
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
             <asp:MultiView ID="MultiView1" runat="server">
                   <asp:View ID="ViewColPrjWise" runat="server">
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
                                                    <asp:Label ID="lblActDesc" runat="server" BackColor="#000066" 
                                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Yellow" Text="Label" Width="250px"></asp:Label>
                                                </td>
                                                <td class="style59">
                                                    <asp:Label ID="Label5" runat="server" CssClass="style50" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="Date:" Width="50px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDate" runat="server" BackColor="#000066" BorderColor="Yellow" 
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="Yellow" Text="Label" Width="150px"></asp:Label>
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
                       
                           
                                            <asp:GridView ID="gvPrjWiseCollBrkDown" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="785px" 
                                                onrowdatabound="gvPrjWiseCollBrkDown_RowDataBound">
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
                                                    <asp:TemplateField HeaderText="Actcode" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActcode" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode"))  %>'    Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Usircode" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvUsircod" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode"))  %>'    Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                      
                                                     <asp:TemplateField HeaderText="Unit Description" FooterText="Total:">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HLgvDesc" runat="server" AutoCompleteType="Disabled" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                                BackColor="Transparent" BorderStyle="None" Font-size="11px" 
                                                                  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))  %>'   
                                                                      Width="280px"></asp:HyperLink>                                      
                                                  
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" ForeColor="White" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HLgvDesc1" runat="server" AutoCompleteType="Disabled" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                                BackColor="Transparent" BorderStyle="None" Font-size="11px" 
                                                                  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname"))  %>'   
                                                                      Width="150px"></asp:HyperLink>                                      
                                                  
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" Font-Size="11px" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                         
                                                    <asp:TemplateField HeaderText="Sales Value">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFSaVal" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvSaVal" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                                    </asp:TemplateField>
                                       

                                                      <asp:TemplateField HeaderText="Encash">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgFClrAmt" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgClrAmt" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" Height="18px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tclramt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                                    </asp:TemplateField>
                                        

                                                 <asp:TemplateField HeaderText="Returned Cheque">
                                    <FooterTemplate><asp:Label ID="lgvFtretamt" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate><asp:Label ID="lgvtretamt" runat="server" Font-Size="11PX" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retcheque")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="right" /></asp:TemplateField>


                                      <asp:TemplateField HeaderText="Today's">
                                    <FooterTemplate><asp:Label ID="lgvFtframt" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate><asp:Label ID="lgvtframt" runat="server" Font-Size="11PX" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcheque")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="right" /></asp:TemplateField>



                                              <asp:TemplateField HeaderText="Post Dated Cheque">
                                    <FooterTemplate><asp:Label ID="lgvFtpdamt" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate><asp:Label ID="lgvtpdamt" runat="server" Font-Size="11PX" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcheque")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="right" /></asp:TemplateField>
                                 







                                                    <asp:TemplateField HeaderText="Total Received Value">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFAmtrep" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvAmtrep" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trecev")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Non-Operating Income">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFNOI" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvNOI" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noiamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="STD Amount">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFStdAmt" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvStdAmt" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Cancel Unit Amount">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFcuamt" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcuamt" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cuamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Received">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFTamt" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvTamt" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-size="11px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                       <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
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
                    <asp:View ID="ViewClientLedger" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel2" runat="server"  BorderColor="Yellow" 
                                         BorderStyle="Solid" BorderWidth="1px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label1" runat="server" CssClass="style50" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="Project Name:" 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td class="style57">
                                                    <asp:Label ID="LblPrjDesc" runat="server" BackColor="#000066" 
                                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Yellow" Text="Label" Width="250px"></asp:Label>
                                                </td>
                                                <td class="style59">
                                                    <asp:Label ID="Label3" runat="server" CssClass="style50" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="Customer Name:" 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCustName" runat="server" BackColor="#000066" BorderColor="Yellow" 
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="Yellow" Text="Label" Width="250px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" CssClass="style50" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="Date:" Width="50px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDate1" runat="server" BackColor="#000066" 
                                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Yellow" Text="Label" Width="150px"></asp:Label>
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
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                       
                           
                                            &nbsp;</td>
                            </tr>
                        </table>
                   </asp:View>
                   <asp:View ID="ViewIndPrjDetails" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel3" runat="server"  BorderColor="Yellow" 
                                         BorderStyle="Solid" BorderWidth="1px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label2" runat="server" CssClass="style50" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="Project Name:" 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td class="style57">
                                                    <asp:Label ID="lblIndPrjDesc" runat="server" BackColor="#000066" 
                                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Yellow" Text="Label" Width="250px"></asp:Label>
                                                </td>
                                                <td class="style59">
                                                    <asp:Label ID="Label7" runat="server" CssClass="style50" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="Date:" Width="50px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblIndDate" runat="server" BackColor="#000066" BorderColor="Yellow" 
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="Yellow" Text="Label" Width="150px"></asp:Label>
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
                       
                           
                                            <asp:GridView ID="gvIndPrjDet" runat="server" AutoGenerateColumns="False" 
                        OnRowDataBound="gvIndPrjDet_RowDataBound" ShowFooter="True">
                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server"  Font-Bold="True" Height="16px" 
                                        Style="text-align: right" 
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCode" runat="server" Height="16px" 
                                        Style="text-align: right" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText=" Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgcActDesc" runat="server"  Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>'  
                                                                          Width="350px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                          
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                               
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                   <asp:View ID="ViewSubLedger" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel4" runat="server"  BorderColor="Yellow" 
                                         BorderStyle="Solid" BorderWidth="1px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label6" runat="server" CssClass="style50" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="Project Name:" 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td class="style57">
                                                    <asp:Label ID="lblLGPrjDesc" runat="server" BackColor="#000066" 
                                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Yellow" Text="Label" Width="250px"></asp:Label>
                                                </td>
                                                <td class="style59">
                                                    <asp:Label ID="Label10" runat="server" CssClass="style50" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="Resource Code:" 
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLGResDesc" runat="server" BackColor="#000066" BorderColor="Yellow" 
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="Yellow" Width="200px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" CssClass="style50" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="Date:" Width="50px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLGDate" runat="server" BackColor="#000066" 
                                                        BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Yellow" Text="Label" Width="150px"></asp:Label>
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
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                       
                           
                               <asp:GridView ID="gvSPLedger" runat="server" AutoGenerateColumns="False"  
                                        ShowFooter="True" onrowdatabound="gvSPLedger_RowDataBound">
                                     <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                    <asp:TemplateField HeaderText="Group Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvGrpDesc" runat="server" style=" text-align:left;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>' width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vou.Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvoudate" runat="server" style=" text-align:left;"
                                                    
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher No.">
                                            <ItemTemplate>                                               
                                                <asp:HyperLink id="HLgvVounum1" runat="server" Width="80px" Font-Bold="true" ForeColor="Black" Font-Size="12px"  style=" text-align:left;"
                                                    Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>' 
                                                    Font-Underline="False" Target="_blank" __designer:wfdid="w1"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Cheque/Ref #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChequeNo" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>' 
                                                    Width="85px"></asp:Label>
                                                    
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldescription0" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>' 
                                                    Width="250px"></asp:Label>
                                                    
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtrnqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtrnrate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'  width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Dr. Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDrAmount0" runat="server" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr. Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCrAmount0" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Balance Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBalamt" runat="server"  Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>

                                              <asp:HyperLink id="HLgvRemarks" runat="server" Width="80px" ForeColor="Black" Font-Size="11px"  style=" text-align:left;"
                                                     Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>' 
                                                    Font-Underline="False" Target="_blank" __designer:wfdid="w1"></asp:HyperLink>


                                               
                                                    
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

