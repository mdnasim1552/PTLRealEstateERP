
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="Default02.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.Default02" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>







<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style6
        {
            width: 82px;
        }
        .style7
        {
            width: 244px;
        }
        .style8
        {
            width: 86px;
        }
        .style9
        {
            width: 248px;
        }
        .style10
        {
        }
        p { margin: 8px; font-size:16px; }
  .selected { color:blue; }
  .highlight { background:yellow; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  
  
  
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>


        
   <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
  <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script src="../JS/RealERPScript.js" type="text/javascript"></script>
   <script language="javascript" type="text/javascript">
       //       $(document).ready(function () {
       //             var gvReqStatus = $('#<%=this.gvReqStatus.ClientID %>');
       //             gvReqStatus.Scrollable();
       //           });
       //





       $(document).ready(function () {


           var stdname = $('#<%=this.DropDownList1.ClientID %>');
           var gvReqStatus = $('#<%=this.gvReqStatus.ClientID %>');
           alert("sdrfd");

           gvReqStatus.Scrollable();

           //           stdname.change(function (e) {
           //               var stdname2 = stdname.val();
           //               alert(stdname2);
           //             

           //           });

       });


       //        function btnChange() {

       //            var stdname = $('#<%=this.DropDownList1.ClientID %>').val();
       //            alert(stdname);
       //           

       //            
       //        }
       //   
       function btnSave() {

           try {
               //var stdname = $('#<%=this.txtstdName.ClientID%>').val();
               var Fathername = $('#<%=this.txtstdfathersName.ClientID %>').val();
               //var stdname1 = document.getElementById('<%=this.DropDownList1.ClientID%>').value;
               var stdname1 = $('#<%=this.DropDownList1.ClientID %>').val();





           } catch (e) { }

           // alert(stdname + "\n" + Fathername);
           alert(stdname1 + "\n" + Fathername);


       }

       function Name() {
           alert("Emdad");
           var gvReqStatus = $('#<%=this.gvReqStatus.ClientID %>');
           gvReqStatus.Scrollable();

       }

    




function btnSubmit_onclick() {

}

   </script>
            <fieldset style="border:1px solid yellow; margin:10px 0 0 0; ">
            <legend>    <asp:Label ID="Label1" runat="server" Text="Input Field" style="font-size:14px; color:White; font-weight:bold;"></asp:Label>
        </legend>
            <table style="width:100%;">
                <tr>
                    <td class="style10">
                        &nbsp;</td>
                    <td class="style6" colspan="2">
                        <asp:Label ID="Label2" runat="server" BorderStyle="None" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" style="text-align: right" 
                            Text="Student Name:" Width="100px"></asp:Label>
                    </td>
                    <td class="style7" colspan="2">
                        <asp:TextBox ID="txtstdName" runat="server" BorderStyle="None" Width="250px"></asp:TextBox>
                    </td>
                    <td class="style8" colspan="2">
                        <asp:Label ID="Label7" runat="server" BorderStyle="None" Font-Bold="True" 
                            Font-Size="14px" ForeColor="Red" style="text-align: right" Text="*"></asp:Label>
                    </td>
                    <td class="style9" colspan="2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            BackColor="Red" ControlToValidate="txtstdName" 
                            ErrorMessage="Please Input User name" Font-Bold="True" Font-Size="12px" 
                            ForeColor="White"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10">
                        &nbsp;</td>
                    <td class="style6" colspan="2">
                        <asp:Label ID="Label4" runat="server" BorderStyle="None" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" style="text-align: right" 
                            Text="Father's Name:" Width="100px"></asp:Label>
                    </td>
                    <td class="style7" colspan="2">
                        <asp:TextBox ID="txtstdfathersName" runat="server" BorderStyle="None" 
                            Width="250px"></asp:TextBox>
                    </td>
                    <td class="style8" colspan="2">
                        &nbsp;</td>
                    <td class="style9" colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10">
                        &nbsp;</td>
                    <td class="style6" colspan="2">
                        <asp:Label ID="Label5" runat="server" BorderStyle="None" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" style="text-align: right" 
                            Text="Mother's Name:" Width="100px"></asp:Label>
                    </td>
                    <td class="style7" colspan="2">
                        <asp:TextBox ID="txtstdMothersName" runat="server" BorderStyle="None" 
                            Width="250px"></asp:TextBox>
                    </td>
                    <td class="style8" colspan="2">
                        &nbsp;</td>
                    <td class="style9" colspan="2">
                      
                        <asp:TreeView ID="MyTreeView" Runat="server" Font-Bold="True" Font-Size="12px" 
                            ForeColor="White" onselectednodechanged="MyTreeView_SelectedNodeChanged">
                            <HoverNodeStyle BackColor="#000066" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Bold="True" Font-Size="12px" ForeColor="Yellow" />
 
</asp:TreeView>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10">
                        &nbsp;</td>
                    <td class="style6" colspan="2">
                        <asp:Label ID="Label6" runat="server" BorderStyle="None" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" style="text-align: right" 
                            Text="District Name:" Width="100px"></asp:Label>
                    </td>
                    <td class="style7" colspan="2">
                        <asp:TextBox ID="txtstddistrictname" runat="server" BorderStyle="None" 
                            Width="250px"></asp:TextBox>
                    </td>
                    <td class="style8" colspan="2">
                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066" 
                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click" 
                            style="width: 19px">Ok</asp:LinkButton>
                    </td>
                    <td class="style9" colspan="2">
                        <input id="btnSubmit" type="submit" value="submit" onclick="btnSave();" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10">
                    <td class="style6" colspan="2">
                        &nbsp;</td>
                    <td class="style7" colspan="2">
                        <asp:DropDownList ID="DropDownList1" runat="server" >
                            <asp:ListItem>emdad</asp:ListItem>
                            <asp:ListItem>nurul</asp:ListItem>
                            <asp:ListItem>shobuz</asp:ListItem>
                            <asp:ListItem>debu</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style8" colspan="2">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="TextBox1">
                        </cc1:CalendarExtender>
                    </td>
                    <td class="style9" colspan="2">
                      
                      
                      
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10">
                        &nbsp;<td class="style6" colspan="2">
                            &nbsp;</td>
                        <td class="style7" colspan="2">
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Width="80px">
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                                <asp:ListItem Value="150">150</asp:ListItem>
                                <asp:ListItem Value="200">200</asp:ListItem>
                                <asp:ListItem Value="300">300</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style8" colspan="2">
                            <asp:Label ID="lblcollection" runat="server"></asp:Label>
                    </td>
                        <td class="style9" colspan="2">
                        <asp:LinkButton ID="lbtnOk0" runat="server" BackColor="#000066" 
                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" onclick="lbtnOk0_Click" 
                            style="height: 17px;">Ok</asp:LinkButton>
                    </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </td>
                </tr>
                <tr>
                    <td class="style10">
                        &nbsp;<td class="style6" colspan="2">
                        <asp:Label ID="Label8" runat="server" BorderStyle="None" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" style="text-align: right" 
                            Text="Project Name:" Width="100px"></asp:Label>
                    </td>
                        <td class="style7" colspan="2">
                            &nbsp;</td>
                        <td class="style8" colspan="2">
                            &nbsp;</td>
                        <td class="style9" colspan="2">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10" colspan="20">
                        <asp:GridView ID="gvReqStatus" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" onpageindexchanging="gvReqStatus_PageIndexChanging" 
                            Width="901px" onChange="Name();" ShowFooter="True">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Height="16px" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" FooterText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesc" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                              
                            </Columns>
                            <PagerStyle BackColor="#666633" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="style10" colspan="2">
                        &nbsp;</td>
                    <td class="style10" colspan="2">



<asp:ListView ID="ListViewProducts" runat="server" ItemPlaceholderID="ProductItem">
    <ItemTemplate>
        <div class="Product">
            <strong>
                <asp:Label runat="server" ID="LabelId" Text='<%# Eval("Id") %>'></asp:Label>
                ::
                <asp:Label runat="server" ID="LabelName" Text='<%# Eval("Name") %>'></asp:Label>
            </strong>
            <br />
            <em>
                <asp:Label runat="server" ID="LabelDescription" Text='<%# Eval("Description") %>'></asp:Label>
            </em>
        </div>
    </ItemTemplate>
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="ProductItem"></asp:PlaceHolder>
    </LayoutTemplate>
    <ItemSeparatorTemplate>
        <hr />
    </ItemSeparatorTemplate>
</asp:ListView> 


    <asp:DataPager ID="DataPagerProducts" runat="server" PagedControlID="ListViewProducts"
    PageSize="3" OnPreRender="DataPagerProducts_PreRender">
    <Fields>
        <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
        <asp:NumericPagerField />
        <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
    </Fields>
</asp:DataPager>
                    </td>
                    <td class="style10" colspan="2">
                        &nbsp;</td>
                    <td class="style10" colspan="2">
                        &nbsp;</td>
                    <td class="style10" colspan="2">
                        &nbsp;</td>
                    <td class="style10" colspan="2">
                        &nbsp;</td>
                    <td class="style10" colspan="2">
                        &nbsp;</td>
                    <td class="style10" colspan="2">
                        &nbsp;</td>
                    <td class="style10" colspan="2">
                        &nbsp;</td>
                    <td class="style10" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10" colspan="20">
                        &nbsp;</td>
                </tr>
            </table>
            </fieldset>
    <%--      </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


