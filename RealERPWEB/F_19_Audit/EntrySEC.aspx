<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntrySEC.aspx.cs" Inherits="RealERPWEB.F_19_Audit.EntrySEC" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style7
        {
        }
        .style16
        {
            text-align: right;
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
        .style25
        {
            width: 31px;
        }
        .style26
        {
            width: 92px;
        }
        .style27
        {
            width: 79px;
        }
        .style28
        {
            width: 12px;
        }
        .style29
        {
            height: 23px;
        }
        .style30
        {
            width: 336px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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


<table style="width: 100%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblHtitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="EMPLOYEE  PERFORMANCE APPRAISAL " Width="432px"
                   STYLE="border-bottom:1px solid WHITE;border-top:1px solid WHITE;" 
                    Height="16px" ></asp:Label>
            </td>
            <td class="style25">
                                                    <asp:Label ID="lblprint" runat="server"></asp:Label>
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
                    onclick="lbtnPrint_Click" CssClass="button" ForeColor="White">PRINT</asp:LinkButton>
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
                    <td class="style7" colspan="12">
                        <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style26">
                                        <asp:Label ID="Label11" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Height="16px" style="TEXT-ALIGN: left" 
                                            Text="Heading:" Width="40px"></asp:Label>
                                    </td>
                                    <td class="style27">
                                        <asp:TextBox ID="txtrefno" runat="server" CssClass="txtboxformat" TabIndex="1" 
                                            Width="500px" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style28">
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
                                    <td class="style30">
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
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style7" colspan="12">
                        <asp:GridView ID="gvSEC" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" Width="337px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                              
                             
                              <asp:TemplateField HeaderText="Condition # ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvconno" runat="server" 
                                           Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "conno")) %>' 
                                            Width="50px" />
                                    </ItemTemplate>

                                    
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                   <asp:TemplateField HeaderText="Description ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDescription" runat="server" 
                                           Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secdesc")) %>' 
                                            Width="300px" />
                                    </ItemTemplate>

                                     <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnUpPerAppraisal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" onclick="lbtnUpPerAppraisal_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Complied">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgvsgval1" runat="server" 
                                           
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssecval1"))=="True" %>' 
                                            Width="70px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText=" Not Complied">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgvsgval2" runat="server" 
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssecval2"))=="True" %>' 
                                           
                                           
                                           
                                           
                                            Width="70px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                  <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                       <asp:TextBox ID="txtgvRemarks" runat="server"  Font-Size="11px" 
                                           Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>' 
                                            Width="150px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
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
                    <td class="style7">
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
                <tr>
                    <td class="style7">
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
                <tr>
                    <td class="style7">
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
                <tr>
                    <td class="style29">
                    </td>
                    <td class="style29">
                    </td>
                    <td class="style29">
                    </td>
                    <td class="style29">
                    </td>
                    <td class="style29">
                    </td>
                    <td class="style29">
                    </td>
                    <td class="style29">
                    </td>
                    <td class="style29">
                    </td>
                    <td class="style29">
                    </td>
                    <td class="style29">
                    </td>
                    <td class="style29">
                    </td>
                    <td class="style29">
                    </td>
                </tr>
                <tr>
                    <td class="style7">
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
                <tr>
                    <td class="style7">
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
                <tr>
                    <td class="style7">
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
                <tr>
                    <td class="style7">
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


