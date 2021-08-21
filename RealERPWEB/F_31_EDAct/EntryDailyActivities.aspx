
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryDailyActivities.aspx.cs" Inherits="RealERPWEB.F_31_EDAct.EntryDailyActivities" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style18
        {
            width: 53px;
        }
        
        .style17
        {
            color: #FFFFFF;
        }
        .style52
        {
            width: 72px;
        }
        .style53
        {
            width: 467px;
        }
        .style54
        {
            width: 170px;
        }
        .style50
    {
        width: 99px;
    }
        .style55
        {
            width: 92px;
        }
        .style56
        {
            width: 1006px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

           
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }

    </script>
    


<table style="width: 61%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="DAILY SALES & COLLECTION STATUS" Width="686px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" ></asp:Label>
            </td>
            <td>    
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
                    onclick="lbtnPrint_Click" style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
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
                        <asp:Panel ID="PnlMain" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style50">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="color: #FFFFFF" Text="Department Name:" Width="120px"></asp:Label>
                                    </td>
                                    <td class="style55">
                                        <asp:TextBox ID="txtSrcDepartment" runat="server" BorderStyle="None" 
                                            CssClass="txtboxformat" Width="80px" TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnFindDepartment" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" TabIndex="2" 
                                            onclick="ibtnFindDepartment_Click" />
                                    </td>
                                    <td class="style56">
                                        <asp:DropDownList ID="ddlDeapartmentName" runat="server" Font-Bold="True" 
                                            Font-Size="12px" TabIndex="3" Width="300px" AutoPostBack="True" 
                                            onselectedindexchanged="ddlDeapartmentName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlDeapartmentName">
                                        </cc1:ListSearchExtender>
                                        <asp:Label ID="lblDeptdesc" runat="server" BackColor="White" 
                                            Font-Size="12px" ForeColor="Blue" Height="16px" Visible="False" 
                                            Width="300px"></asp:Label>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" onclick="lbtnOk_Click" 
                                            style="color: #FFFFFF; text-align: center;" TabIndex="8" Width="45px">Ok</asp:LinkButton>
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
                                <tr>
                                    <td class="style50">
                                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="color: #FFFFFF" Text=" Activities:" Width="120px"></asp:Label>
                                    </td>
                                    <td class="style55">
                                        <asp:TextBox ID="txtSrcDepAc" runat="server" BorderStyle="None" 
                                            CssClass="txtboxformat" Width="80px" TabIndex="4"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnFindDepActvities" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" TabIndex="5" 
                                            onclick="ibtnFindDepActvities_Click" />
                                    </td>
                                    <td class="style56">
                                        <asp:DropDownList ID="ddlActivities" runat="server" Font-Bold="True" Font-Size="12px" 
                                            TabIndex="6" Width="300px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblActivitiesdesc" runat="server" BackColor="White" 
                                            Font-Size="12px" ForeColor="Blue" Height="16px" Visible="False" 
                                            Width="300px"></asp:Label>
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
                                <tr>
                                    <td class="style50">
                                        <asp:Label ID="Label10" runat="server" CssClass="style17" Font-Bold="True" 
                                            Font-Size="12px" Height="16px" style="TEXT-ALIGN: LEFT" Text="Date:" 
                                            Width="60px"></asp:Label>
                                    </td>
                                    <td class="style55">
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" 
                                            BorderStyle="None" Width="80px" TabIndex="7"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="style56">
                                        &nbsp;<asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
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
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    </tr>
                <tr>
                    <td colspan="12">
                        <asp:MultiView ID="MultiView1" runat="server">

                                      <asp:View ID="ViewDailyReport" runat="server">
                                          <table style="width: 100%;">
                                              <tr>
                                                  <td colspan="12">
                                                      <asp:GridView ID="gvDailyEntry" runat="server" AutoGenerateColumns="False" 
                                                          ShowFooter="True" Width="531px">
                                                          <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                          <Columns>
                                                              <asp:TemplateField HeaderText="Sl.No.">
                                                                  <ItemTemplate>
                                                                      <asp:Label ID="serialnoidy0" runat="server" Style="text-align: right" 
                                                                          Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                  </ItemTemplate>
                                                                  <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                              </asp:TemplateField>
                                                              

                                                              <asp:TemplateField HeaderText="Activities">
                                                                  
                                                                    <FooterTemplate>
                                                                      <asp:LinkButton ID="lbtnUpDailyEntry" runat="server" Font-Bold="True" 
                                                                          Font-Size="12px" ForeColor="White" onclick="lbtnUpDailyEntry_Click" 
                                                                          style="text-decoration:none;">Update</asp:LinkButton>
                                                                  </FooterTemplate>

                                                                  <ItemTemplate>
                                                                      <asp:Label ID="lblgvactivities" runat="server" BorderColor="#99CCFF" 
                                                                          BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                                          style="text-align: Left; background-color: Transparent" 
                                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gendesc")) %>' 
                                                                          Width="100px"></asp:Label>
                                                                  </ItemTemplate>
                                                                  <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                                              </asp:TemplateField>


                                                              
                                                              <asp:TemplateField HeaderText="Unit">
                                                                  
                                                                  <ItemTemplate>
                                                                      <asp:Label ID="lblgvunit" runat="server" BorderColor="#99CCFF" 
                                                                          BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                                          style="text-align: Left; background-color: Transparent" 
                                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                          Width="100px"></asp:Label>
                                                                  </ItemTemplate>
                                                                  <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                                              </asp:TemplateField>

                                                             
                                                                <asp:TemplateField HeaderText="Particulars">
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtgvpertuculars" runat="server" BorderColor="#99CCFF" 
                                                                          BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                                          style="text-align: right; background-color: Transparent" 
                                                                          Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prticlars")).ToString("#,##0;-#,##0; ")%>' 
                                                                          Width="80px"></asp:TextBox>
                                                                  </ItemTemplate>
                                                                  <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                                                      VerticalAlign="Middle" />
                                                                  <ItemStyle HorizontalAlign="Right" />
                                                                  <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                                              </asp:TemplateField>



                                                               <asp:TemplateField HeaderText="Remarks">
                                                                  <ItemTemplate>
                                                                      <asp:TextBox ID="txtgvremarks" runat="server" BorderColor="#99CCFF" 
                                                                          BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                                          style="text-align: left; background-color: Transparent" 
                                                                          Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks"))%>' 
                                                                          Width="200px"></asp:TextBox>
                                                                  </ItemTemplate>
                                                                  <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                                                      VerticalAlign="Middle" />
                                                                  <ItemStyle HorizontalAlign="left" />
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style54">
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

