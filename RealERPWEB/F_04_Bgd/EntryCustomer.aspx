<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryCustomer.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.EntryCustomer" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style8
        {
            width: 74px;
        }
        .style9
        {
            width: 48px;
        }
        .style10
        {
            width: 7px;
        }
        .style13
        {
            width: 761px;
            
        }
        
             .AutoExtender
       {
            font-family: Verdana, Helvetica, sans-serif;
            margin:0px 0 0 0px;
            font-size: 11px;
            font-weight: normal;
            border: solid 1px #006699;
          
            background-color: White;
           
           
        }
        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            $('#tblhead').hide();


        });

      function funSeachProject() {




       var ddlProject = $('#<%=this.ddlProject.ClientID %>');
          var tbl = $('#tblhead');
        ddlProject.children('option').remove();
          tbl.html('');
       var txtSechProName ='%'+ $('#<%=this.txtserchProject.ClientID %>').val()+'%';
          var RealErpObj = new RealERPScript();
          var list = RealErpObj.GetProjectName(txtSechProName);
          tbl.append('<thead><tr><th style="color:white;border:1px solid white;">' + 'PactCode' + '</th><th style="color:white; border:1px solid white; width:400px;">' + 'Pactdesc' + '</th></tr> </thead>');
          $.each(list, function (index, list) {
           ddlProject.append('<option value="' + list.pactcode + '">' + list.pactdesc + '</option>');
              tbl.append('<tr><td style="color:white; border:1px solid white;">' + list.pactcode + '</td><td style="color:white; border:1px solid white; width:400px;">' + list.pactdesc + '</td></tr>');


          });

          tbl.append('<tfoot><tr><th style="color:white; border:1px solid white;">' + "Total" + '</th><th style="color:white; border:1px solid white; width:400px;"></th></tr></tfoot>');
          $('#tblhead tr:odd').css("background-color", "yellow");
          $('#tblhead tr:odd td').css("color", "black");
          tbl.show();
          alert("dsfsdf ds");
          
       }
      
    </script>


<table style="width: 99%;">
        <tr>
            <td class="style14">
                <asp:Label ID="lblHeadtitle" runat="server" Font-Bold="True" Font-Size="16px" 
                    ForeColor="Yellow" Text="MASTER BUDGET INFORMATION" Width="629px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" 
                    Height="16px" ></asp:Label>
            </td>
            <td class="style15">
                                    <asp:Label ID="lbljavascript" runat="server" ClientIDMode="Static"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td class="style27">
                &nbsp;</td>
            <td class="style27">
                &nbsp;</td>
            <td class="style27">
                </td>
        </tr>
        
        </table>
 
                
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%; ">
                <tr>
                    <td class="style8">
                        <asp:Label ID="Label4" runat="server" BorderStyle="None" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" style="text-align: right" 
                            Text="Project Name:" Width="100px"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:TextBox ID="txtserchProject" runat="server" BorderStyle="None" 
                            Width="80px"></asp:TextBox>
                    </td>
                    <td class="style10">
                        
                        <input id="Button1" type="button" onclick="javascript:funSeachProject();" style="background-image: url('../Image/SearchImage.jpg'); width: 28px; height: 24px; border-style: none;" />
                        
                    </td>
                    <td class="style13">
                        <asp:DropDownList ID="ddlProject" runat="server" Width="300px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Ok</asp:LinkButton>
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
                <tr>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style9">
                        &nbsp;</td>
                    <td class="style10">
                        &nbsp;</td>
                    <td class="style13">
                       
                        <table id="tblhead" >
                         
                           <thead>
                               <tr>
                                   <th>
                                       Pactcode</th>
                                   <th>
                                       Pactdesc</th>
                               </tr>
                            </thead>
                            <tbody><tr><td></td><td></td></tr>
                            <tr><td></td><td></td></tr>
                            </tbody>

                            <tfoot> <tr>
                                   <th>
                                      Total</th>
                                   <th>
                                       </th>
                               </tr></tfoot>
                            
                        </table>
                    </td>
                    <td>
                        <img src="../Image/loading_circle.gif"  width="80px" height="90px" alt="not foound"/>
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
                <tr>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style9">
                        &nbsp;</td>
                    <td class="style10">
                        &nbsp;</td>
                    <td class="style13">
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


