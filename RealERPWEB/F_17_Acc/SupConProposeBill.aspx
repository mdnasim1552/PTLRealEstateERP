<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SupConProposeBill.aspx.cs" Inherits="RealERPWEB.F_17_Acc.SupConProposeBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
    
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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

            $('#tblrpProBill').Scrollable({

            });
            //var rpview = $('#tblrpProBill');
            //$.keynavigation('#tblrpProBill');
           


        };
    </script>

  


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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">

                                            <asp:Label ID="Label9" runat="server" CssClass="lblName lblTxt" Text="Sub Contractor:"></asp:Label>

                                            <asp:TextBox ID="txtSearchContractor" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="imgbtnFindContractor" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindContractor_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlContractorName" runat="server" CssClass=" ddlPage" Width="300px" TabIndex="2"></asp:DropDownList>

                                            <asp:Label ID="lblContractorName" runat="server" Visible="False" Width="300px" CssClass="inputtextbox"></asp:Label>

                                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>


                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">

                                            <asp:Label ID="Label5" runat="server" CssClass="lblName lblTxt" Text="From:"></asp:Label>

                                            <asp:TextBox ID="txtFDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to" Text="To:"></asp:Label>

                                            <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">

                                  

                                           

                                            <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="Pro. Date:"></asp:Label>

                                            <asp:TextBox ID="txtProDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtProDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtProDate"></cc1:CalendarExtender>


                                            <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>


                                        </div>

                                    </div>

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>




                    <asp:Repeater ID="rpProBill" runat="server" OnItemDataBound="rpProBill_ItemDataBound">

                        <HeaderTemplate>
                            <table id="tblrpProBill" class=" table-striped table-hover table-bordered grvContentarea">
                                <tr>
                                    <th style="width:20px;">SL</th>
                                    <th style="width:80px;">Code</th>
                                    <th style="width:350px;">Contractor Name</th>
                                    <th style="width:70px;">Closing Bill</th>
                                    <th style="width:70px;">Closing Advanced</th>
                                    <th style="width:70px;">Proposal Amt</th>
                                      <th style="width:70px;">Approved Amt</th>

                                    
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <tr>
                                <td>
                                    <asp:Label ID="lblrpSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </td>

                                <td>
                                    <asp:Label ID="lblrpCode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                        Width="80px"></asp:Label>
                                </td>

                                <td>
                                    <asp:Label ID="lblrpAcDesc" runat="server"
                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                        Width="350px">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblrpClsAmt" runat="server" Font-Size="11px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsbill")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblrpClsAdv" runat="server" Font-Size="11px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsadv")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtrpProamt" runat="server" Font-Size="11px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px" BorderStyle="None"></asp:TextBox></td>
                           

                                <td>
                                    <asp:TextBox ID="txtgvAproamt" runat="server" Font-Size="11px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apramt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px" BorderStyle="None"></asp:TextBox></td>
                                
                                
                                 </tr>

                        </ItemTemplate>

                        <FooterTemplate>

                            <tr>
                                <th></th>
                                <th>
                                   <%-- <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn  btn-primary primaryBtn">Total</asp:LinkButton>  --%>

                                </th>
                                <th>
                                <%--<asp:LinkButton ID="lFinalUpdate" runat="server" OnClick="lFinalUpdate_Click" CssClass="btn  btn-danger primaryBtn">Final Update</asp:LinkButton>--%>
                                </th>
                                <th>
                                    <asp:Label ID="lblrpFClsBill" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Width="70px"></asp:Label></th>
                                   <%-- <asp:Label ID="lblrpFClsAmt" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Width="70px"></asp:Label></th>--%>

                                <th>
                                    <asp:Label ID="lblrpFClsAdv" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Width="70px"></asp:Label></th>
                                <th>
                                    <asp:Label ID="lblrpFProamt" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Width="70px"></asp:Label></th>


                                 <th>
                                    <asp:Label ID="lblgvFAproamt" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Width="70px"></asp:Label></th>

                            </tr>


                            </table>
                        </FooterTemplate>





                    </asp:Repeater>





                   
                </div>
            </div>



            <%--<tr>
                                                <td class="style47">
                                                    </td>
                                                <td class="style45">
                                                    <asp:Label ID="Label9" runat="server" CssClass="style42" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="Sub Contractor:" 
                                                        Width="90px"></asp:Label>
                                                </td>
                                                <td class="style46">
                                                    <asp:TextBox ID="txtSearchContractor" runat="server" CssClass="txtboxformat" 
                                                        Width="80px"></asp:TextBox>
                                                </td>
                                                <td class="style36">
                                                    <asp:ImageButton ID="imgbtnFindContractor" runat="server" Height="17px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindContractor_Click" 
                                                        Width="16px" TabIndex="1" />
                                                </td>
                                                <td class="style71" colspan="3">
                                                    <asp:DropDownList ID="ddlContractorName" runat="server" 
                                                        Font-Bold="True" Font-Size="12px" Width="300px" TabIndex="2">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblContractorName" runat="server" __designer:wfdid="w4" 
                                                        BackColor="#000" Font-Bold="True" Font-Size="14px" ForeColor="Maroon" 
                                                        style="FONT-SIZE: 12px; TEXT-ALIGN: left" Visible="False" Width="300px"></asp:Label>
                                                </td>
                                                <td class="style39">
                                                    <asp:LinkButton ID="lnkbtnOk" runat="server" BackColor="#000066" 
                                                        BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="#000" onclick="lnkbtnOk_Click" 
                                                        style="text-align: center" TabIndex="7" Text="Ok" Width="60px"></asp:LinkButton>
                                                </td>
                                                <td class="style40">
                                                    </td>
                                                <td>
                                                    </td>
                                                <td>
                                                    </td>
                                            </tr>--%>
            <%--<tr>
                                                <td class="style47">
                                                </td>
                                                <td class="style45">
                                                    <asp:Label ID="Label5" runat="server" CssClass="style42" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="From:" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style46">
                                                    <asp:TextBox ID="txtFDate" runat="server" CssClass="txtboxformat" Width="80px" 
                                                        TabIndex="3"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                        Format="dd-MMM-yyyy" TargetControlID="txtFDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="style36">
                                                    <asp:Label ID="Label6" runat="server" CssClass="style42" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: left" Text="To:" Width="55px"></asp:Label>
                                                </td>
                                                <td class="style71">
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat" Width="80px" 
                                                        TabIndex="4"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                                        Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="style71">
                                                    &nbsp;</td>
                                                <td class="style73">
                                                    &nbsp;</td>
                                                <td class="style39">
                                                    &nbsp;</td>
                                                <td class="style40">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>--%>
            <%--<tr>
                <td class="style47"></td>
                <td class="style45">
                    <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px"
                        Style="color: #FFFFFF; text-align: left;" Text="Page Size:" Visible="False"
                        Width="80px"></asp:Label>
                </td>
                <td class="style46">
                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                        BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                        Width="80px" TabIndex="5">
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
                <td class="style36">
                    <asp:Label ID="Label8" runat="server" CssClass="style42" Font-Bold="True"
                        Font-Size="12px" Style="text-align: right" Text="Pro. Date:"
                        Width="55px"></asp:Label>
                </td>
                <td class="style71">
                    <asp:TextBox ID="txtProDate" runat="server" CssClass="txtboxformat"
                        Width="80px" TabIndex="6"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtProDate_CalendarExtender" runat="server"
                        Format="dd-MMM-yyyy" TargetControlID="txtProDate"></cc1:CalendarExtender>
                </td>
                <td class="style71">&nbsp;</td>
                <td class="style73">&nbsp;</td>
                <td class="style39"></td>
                <td class="style40"></td>
                <td>
                    <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True"
                        Font-Size="12px" ForeColor="Maroon" Style="color: #FFFFFF"></asp:Label>
                </td>
                <td></td>
            </tr>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



