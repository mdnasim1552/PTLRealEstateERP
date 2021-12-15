<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ViewAllTemplate.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.ViewAllTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="card card-fluid container-data mt-5" id='printarea' style="min-height: 600px;">
        <div class="card-body">
            <div class="row">
                                    <asp:LinkButton ID="lnkbtnAddNew" runat="server"  OnClick="lnkbtnAddNew_Click" CssClass="btn btn-primary okBtn  pull-right">+ Add Template</asp:LinkButton>

            </div>
            <div class="clearfix"></div>
            <br />
            <!-- row -->
          <div class="table table-responsive">
                    <asp:GridView ID="GvTemplate" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" AllowPaging="true" OnPageIndexChanging="GvTemplate_PageIndexChanging" PageSize="20" >
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblslno" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="5px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SMS For">
                                <ItemTemplate>
                                    <asp:Label ID="lblSmsFor" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "smsforcon")) %>'></asp:Label>
                                      <asp:Label ID="lblSmsId" runat="server" Visible="false"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Template Bangla">
                                <ItemTemplate>
                                    
                                    <asp:Label ID="lblTempBn"  runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tempban")) %>' Width="220px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Template English">
                                <ItemTemplate>
                                    <asp:Label ID="lblTemEn" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tempeng")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SMS Format">
                                <ItemTemplate>
                                    <asp:Label ID="lblemail" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "smsformattext")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Create Date">
                                <ItemTemplate>

                                    <asp:Label ID="lblCreateDate" runat="server" Text='<%#(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).Year==1900 ? "": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy hh:MM tt")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkedit" runat="server"  ToolTip="Edit" OnClick="lnkedit_Click" CssClass="btn-link btn-sm bcontrol" Text="Edit"><span class="glyphicon glyphicon-edit"></span> Edit</asp:LinkButton>
                                    

                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>


                        </Columns>

                        <EmptyDataTemplate>
                            <div style="color: red; text-align: center !important; font-style: italic; font-size: 15px;">No records to display.</div>
                        </EmptyDataTemplate>

                    </asp:GridView>
                </div>

            </div>
          </div>
</asp:Content>
