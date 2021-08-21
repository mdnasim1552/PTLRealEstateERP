<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MonthlyTarget.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_99_MgtAct.MonthlyTarget" %>
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
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                        <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style52">
                                        <asp:Label ID="Label10" runat="server" CssClass="style17" Font-Bold="True" 
                                            Font-Size="12px" Height="16px" style="TEXT-ALIGN: LEFT" Text=" Month:" 
                                            Width="60px"></asp:Label>
                                    </td>
                                    <td class="style18">
                                        <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="12px" TabIndex="11" Width="95px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style23">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click" 
                                            style="text-align: center; height: 17px;" Width="60px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style53">
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
                        <asp:GridView ID="gvSalbgd" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" Width="531px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbTotal0" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lbTotal_Click" style="text-decoration:none;"> Total </asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnFinalUpdate0" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnFinalUpdate_Click" 
                                            style="text-decoration:none;">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDepartment0" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            style="text-align: Left; background-color: Transparent" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>' 
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFSaleTotal0" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvsalamt0" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            style="text-align: right; background-color: Transparent" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saleamt")).ToString("#,##0;-#,##0; ") %>' 
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                        VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Collection">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCollTotal0" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcollamt0" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            style="text-align: right; background-color: Transparent" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;-#,##0; ") %>' 
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                        VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Today's Sale">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoSaleTotal0" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtogvtosalamt0" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            style="text-align: right; background-color: Transparent" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsaleamt")).ToString("#,##0;-#,##0; ") %>' 
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                        VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Today's Collection">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoCollTotal0" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvtocollamt0" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            style="text-align: right; background-color: Transparent" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcollamt")).ToString("#,##0;-#,##0; ") %>' 
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                        VerticalAlign="Middle" />
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

