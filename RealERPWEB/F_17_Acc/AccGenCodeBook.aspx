﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccGenCodeBook.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccGenCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
div#ContentPlaceHolder1_ddlGenCodeBook_chzn {
            width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlDepartment_chzn {
            width: 100% !important;
        }

        .mt20 {
            margin-top: 20px;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="LblBookName1" runat="server" Text=" SCode Book"></asp:Label>

                                <asp:DropDownList ID="ddlGenCodeBook" runat="server" Width="400px" CssClass="form-control chzn-select"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="LblBookName2" runat="server" Text="Search Option">
                                    <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click"  Visible="false"><i class="fa fa-search"> </i></asp:LinkButton>
                                </asp:Label>
                                <asp:TextBox ID="txtsrch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="lblName lblTxt" Text="Details Code:"></asp:Label>
                                <asp:DropDownList ID="ddlOthersBookSegment" runat="server" CssClass="form-control form-control-sm">
                                    <asp:ListItem Value="2">Main Code</asp:ListItem>
                                    <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                    <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="8">Details Code</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblGenCode" runat="server" Visible="False" Width="400px"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" Text="Page Size"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" form-control form-control-sm"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <asp:Label ID="lblGenCode0" runat="server"  Visible="False"></asp:Label>
                            <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lnkok_Click">Ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                                <div class="table table-responsive">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False"
                            CellPadding="4" Font-Size="12px" OnPageIndexChanging="grvacc_PageIndexChanging"
                            OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" PageSize="15" Width="576px">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                                Visible="False" />
                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" ">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gencode1")) %>'
                                            Width="40px"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gencode1")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                            MaxLength="5"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gencode2")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gencode2")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gendesc")) %>'
                                            Width="325px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <%--    <HeaderTemplate >
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Description of Code" Width="150px"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True"
                                            Font-Bold="True" Font-Size="14px"
                                            OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                            Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                            Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gendesc")) %>'
                                            Width="325px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Type Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvsirtdesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gentdesc")) %>'
                                            Width="150px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltypedesc" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gentdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />


                        </asp:GridView>
                    </div>
                </div>
            </div>

            <%--     
                        <fieldset class="scheduler-border fieldset_A">
                            
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel2" runat="server">

                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">

                                            <asp:Label ID="LblBookName1" runat="server" CssClass="lblName lblTxt" Text=" SCode Book:"></asp:Label>

                                            <asp:DropDownList ID="ddlGenCodeBook" runat="server" Width="400px" CssClass="ddlPage"></asp:DropDownList>

                                            <asp:Label ID="lblGenCode" runat="server" CssClass=" inputtextbox" Visible="False" Width="400px"></asp:Label>

                                            <asp:DropDownList ID="ddlOthersBookSegment" runat="server" CssClass="ddlPage">
                                                <asp:ListItem Value="2">Main Code</asp:ListItem>
                                                <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                                <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="8">Details Code</asp:ListItem>
                                            </asp:DropDownList>

                                        
                                        <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Text="Page Size"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" ddlPage" style="width:120px;"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                        </asp:DropDownList>
                                  





                                        </div>
                                    </div>

                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">

                                               
                                                </div>
                                              <div class="col-md-3  pull-right">
                                                <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                                  </div>
                                            </div>
                                        </div>

                                    </asp:Panel>


                                </asp:Panel>
                         
                        </fieldset--%>>
                 
        


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

