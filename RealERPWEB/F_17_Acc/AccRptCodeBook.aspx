<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccRptCodeBook.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccRptCodeBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  

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
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">

                                            <asp:Label ID="LblBookName1" runat="server" CssClass="lblName lblTxt" Text="Code Book:"></asp:Label>
                                            <a href="AccRptCodeBook.aspx">AccRptCodeBook.aspx</a>
                                            <asp:DropDownList ID="ddlOthersBook" runat="server" Width="300px" CssClass="ddlPage"></asp:DropDownList>

                                            <asp:Label ID="lbalterofddl" runat="server" CssClass=" inputtextbox" Visible="False" Width="300px">></asp:Label>

                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="lblPage" runat="server" CssClass="lblName lblTxt" Text="Size:"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="85px">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="grvaccRp" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" OnPageIndexChanging="grvaccRp_PageIndexChanging"
                            OnRowCancelingEdit="grvaccRp_RowCancelingEdit" OnRowEditing="grvaccRp_RowEditing"
                            OnRowUpdating="grvaccRp_RowUpdating" AllowPaging="True">

                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtn" ImageUrl="~/Image/Edit.jpg" runat="server" Width="25" Height="25" OnClick="imgbtn_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode2"))+"-" %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                            MaxLength="7"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode3")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode3")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpdesc")) %>'
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>








                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode")) %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                    </div>
                    <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-md-8 pading5px asitCol8">

                            <asp:Label ID="lblresult" runat="server" ></asp:Label>

                            <asp:Button ID="btnShowPopup" runat="server"  CssClass="btn btn-primary"/>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
                                CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                        </div>
                    </div>
                         <asp:Panel ID="pnlpopup" runat="server" >
                               <div class="form-group">
                        <div class="col-md-8 pading5px asitCol8">

                              <asp:Label ID="lblID" runat="server"></asp:Label>
                             <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Update" OnClick="btnUpdate_Click"  CssClass="btn btn-danger primaryBtn"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger primaryBtn"/>

                             <asp:Label ID="lblGrpCode" runat="server" ></asp:Label>

                             <asp:TextBox ID="txtCode2" runat="server" MaxLength="6" />

                              <asp:TextBox ID="txtDesc" runat="server" />
                           
                        </div>
                    </div>
                         

                         </asp:Panel>

                </div>

                </div>
            </div>


            <%--<tr>
                    <td class="style22">
                        <asp:Label ID="LblBookName1" runat="server" BorderStyle="None" Font-Bold="True" 
                            Font-Size="12px" ForeColor="#003366" Height="12px" 
                            style="FONT-SIZE: 12px; TEXT-ALIGN: right; color: #FFFFFF;" Text="SELECT CODE BOOK :" 
                            Width="135px" CssClass="style15"></asp:Label>
                    </td>
                    <td class="style21">
                        <asp:DropDownList ID="ddlOthersBook" runat="server" BackColor="#68AED0" 
                        Font-Bold="True" Font-Size="12px" style="margin-left: 0px" Width="300px">
                        </asp:DropDownList>
                        <asp:Label ID="lbalterofddl" runat="server" BackColor="#68AED0" 
                        BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                        Font-Size="12px" style="margin-bottom:1px" Visible="False" Width="300px"></asp:Label>
                    </td>
                    <td class="style20">
                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click" 
                            style="text-align: center" Width="50px">Ok</asp:LinkButton>
                    </td>
                    <td class="style50">
                        &nbsp;</td>
                    
                </tr>--%>
            <%--<tr>
                   <td class="style22">
                       <asp:Label ID="lblPage" runat="server" CssClass="style18" Font-Bold="True"
                           Font-Size="12px" ForeColor="White" Text="Size:" Width="135px"></asp:Label>
                   </td>
                   <td class="style21">
                       <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                           BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                           OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="85px">
                           <asp:ListItem Value="10">10</asp:ListItem>
                           <asp:ListItem Value="20">20</asp:ListItem>
                           <asp:ListItem Value="30">30</asp:ListItem>
                           <asp:ListItem Value="50">50</asp:ListItem>
                           <asp:ListItem Value="100">100</asp:ListItem>
                           <asp:ListItem Value="150">150</asp:ListItem>
                           <asp:ListItem Value="200">200</asp:ListItem>
                           <asp:ListItem Value="300">300</asp:ListItem>
                       </asp:DropDownList>
                   </td>
                   <td class="style20">
                       <asp:Label ID="ConfirmMessage" runat="server" BackColor="Red"
                           Font-Italic="False" Font-Size="12px" ForeColor="White"></asp:Label>
                   </td>
                   <td class="style50"></td>

               </tr>--%>

            <%--<asp:GridView ID="grvacc" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BorderColor="SteelBlue" BorderStyle="Solid" 
                            BorderWidth="2px" CellPadding="4" Font-Size="12px" 
                            onrowcancelingedit="grvacc_RowCancelingEdit" onrowediting="grvacc_RowEditing" 
                            onrowupdating="grvacc_RowUpdating" ShowFooter="True">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" 
                                Visible="False" />
                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" /> 
                            
                            
                            <Columns>--%>



            <%--   <asp:Label ID="lblresult" runat="server" />
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
                CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
           BackColor="White" Height="180px" Width="550px" Style="display: none">
                <table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #D55500">
                        <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger" align="center">Reporting Code Details</td>
                    </tr>
                    <%--<tr>
<td align="right" style=" width:45%">
Sl No:
</td>
<td>
<asp:Label ID="lblID" runat="server"></asp:Label>
</td>
</tr>
            --%>

            <%--<tr>
                <td align="right">GRoup Code : 
                </td>
                <td>
                    <asp:Label ID="lblGrpCode" runat="server"></asp:Label>
                </td>
            </tr>--%>
            <%--<tr>
                <td align="right">Code:
                </td>
                <td>
                    <asp:TextBox ID="txtCode2" runat="server" MaxLength="6" />
                </td>
            </tr>--%>
            <%--<tr>
                <td align="right">Description of Code : 
                </td>
                <td>
                    <asp:TextBox ID="txtDesc" runat="server" />
                </td>
            </tr>--%>
            <%--<tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                        </td>
                    </tr>--%>
                </table>
            </asp:Panel>
            </td>
                </tr>
                <tr>

                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="style50">&nbsp;</td>
                    <td class="style48">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




