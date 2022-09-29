<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="Tranlimitdate.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.Tranlimitdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


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

            <div class="card mt-4">
                <div class="card-header">
                    <div class="row">
                            
                               <div class="col-md-1">
                                            <asp:LinkButton ID="lbtnOk0" runat="server" CssClass="btn  btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    <asp:Label ID="lmsg" Visible="false" runat="server" CssClass="pull-right btn btn-danger primaryBtn"></asp:Label>
                                        </div>
                       
                              
                    </div>
                    </div>
                </div>
            <div class="card">
                <div class="card-header">
                    <div class="row">
                         
                        <asp:GridView ID="gvcomlimit" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvcomlimit_RowDataBound">
                            <RowStyle />
                            <Columns>

                                <asp:TemplateField HeaderText="Company Name">

                                    <ItemTemplate>
                                        <asp:Label ID="lblcompany" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "company")) %>'
                                            Width="150px" Style="text-align: left"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Back Day">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lUpdatPerInfo" runat="server"  CssClass="btn btn-danger btn-xs"
                                          OnClick="lUpdatPerInfo_Click"
                                            Style="text-decaration: none;">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvlimit" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bday")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px" Style="text-align: left"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Upto Time">                                   
                                   
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlhourPart" runat="server" >
                                        </asp:DropDownList>
                                        :
                                         <asp:DropDownList ID="ddlminpart" runat="server" >
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlampmpart" runat="server" >
                                            <asp:ListItem Value="PM">PM</asp:ListItem>
                                            <asp:ListItem Value="AM">AM</asp:ListItem>
                                        </asp:DropDownList>




                                       
                                        
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    
                    </div>
            </div>

                  
                </div>
           



            
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





