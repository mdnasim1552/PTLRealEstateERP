﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="FinCodeBook.aspx.cs" Inherits="RealERPWEB.F_03_Fin.FinCodeBook" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-4 pading5px asitCol3" style="width: 220px">

                                            <asp:Label ID="LblBookName1" runat="server" CssClass="lblTxt lblName" Width="120px">SELECT CODE BOOK</asp:Label>


                                            <asp:DropDownList ID="ddlSalPayment" runat="server" CssClass="ddlPage">
                                            </asp:DropDownList>
                                            <asp:Label ID="lbalterofddl" runat="server" CssClass="lblTxt lblName" Visible="False"></asp:Label>



                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <asp:DropDownList ID="ddlOthersBookSegment" runat="server" CssClass="ddlPage">
                                                <asp:ListItem Value="2">Sub Code-1</asp:ListItem>

                                                <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lbalterofddl0" runat="server" CssClass="lblTxt lblName" Visible="False"></asp:Label>


                                            <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary primaryBtn"
                                                OnClick="lnkok_Click">Ok</asp:LinkButton>
                                        </div>

                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </div>



                    <%--<table class="style17">
                <tr>
                    <td>
                        <asp:Panel ID="Panel4" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                                <table width="750px" style="height: 49px">

                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblBookName1" runat="server" BorderStyle="None" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#003366" Height="16px" Style="text-align: right;
                                                color: #FFFFFF;" Text="SELECT CODE BOOK :" Width="198px" 
                                                CssClass="style15"></asp:Label>
                                        </td>
                                        <td style="height:16px; vertical-align:middle;">
                                            <asp:DropDownList ID="ddlSalPayment" runat="server" Font-Bold="True" Font-Size="15px"
                                                Style="margin-left: 0px" Width="400px">
                                            </asp:DropDownList>
                                           
                                             <asp:Label ID="lbalterofddl" runat="server" BackColor="#68AED0" Font-Size="15px"
                                              BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                Style="margin-bottom: 1px" Visible="False" Width="400px"></asp:Label>
                                        </td>
                                        <td style="height:16px; vertical-align:middle;">
                                            <asp:DropDownList ID="ddlOthersBookSegment" runat="server" Font-Bold="True" Font-Size="16px"
                                                Style="margin-left: 0px" Width="129px">
                                                <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                                            </asp:DropDownList>
                                            
                                            <asp:Label ID="lbalterofddl0" runat="server" BackColor="#68AED0" 
                                            BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="14px" style="margin-bottom:1px" Visible="False" Width="130px"></asp:Label>
                                        </td>
                                        <td class="style23">
                                            <asp:LinkButton ID="lnkok" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                                OnClick="lnkok_Click" Width="43px" CssClass="style15">OK</asp:LinkButton>
                                        </td>
                                        
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="ConfirmMessage" runat="server"  Font-Size="12px"
                            Font-Bold="True" ForeColor="White" BackColor="Red"></asp:Label>
                    </td>
                    
                </tr>--%>
                    <tr>
                        <td colspan="7">
                            <%-- <asp:GridView ID="gvPaySch" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BorderColor="SteelBlue" BorderStyle="Solid" 
                            BorderWidth="2px" CellPadding="4" Font-Size="12px" 
                            onrowcancelingedit="gvPaySch_RowCancelingEdit" onrowediting="gvPaySch_RowEditing" 
                            onrowupdating="gvPaySch_RowUpdating" ShowFooter="True">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" 
                                Visible="False" />
                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" /> 
                            --%>
                            <asp:GridView ID="gvPaySch" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BorderColor="SteelBlue" BorderStyle="Solid" BorderWidth="2px" CellPadding="4"
                                Font-Size="12px" OnRowCancelingEdit="gvPaySch_RowCancelingEdit" OnRowEditing="gvPaySch_RowEditing"
                                OnRowUpdating="gvPaySch_RowUpdating" PageSize="15" Width="284px" ShowFooter="True">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Visible="False" />
                                <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" SelectText=""
                                        ShowEditButton="True">
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod2"))+"-" %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px" MaxLength="6"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>'
                                                Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100" Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="250px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True" Font-Bold="True"
                                                Font-Size="14px" OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged" Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                Width="150px">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                Visible="False"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
                                                Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#CAD8B1" Height="15px" />
                                <EditRowStyle BackColor="LightSkyBlue" Font-Bold="True" Font-Size="12px" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#999999" Font-Bold="True" Font-Size="16px" ForeColor="White"
                                    HorizontalAlign="Right" />
                                <HeaderStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#66CCFF" />
                            </asp:GridView>
                            <%--<RowStyle BackColor="#92AF5F" Height="15px" />
                            <EditRowStyle BackColor="LightSkyBlue" Font-Bold="True" Font-Size="12px" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#999999" Font-Bold="True" Font-Size="16px" 
                                ForeColor="White" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#BBC684" />
                        </asp:GridView>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td class="style50">&nbsp;
                        </td>
                        <td class="style48">&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
