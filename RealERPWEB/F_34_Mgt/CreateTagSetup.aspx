<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CreateTagSetup.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.CreateTagSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card card-fluid container-data mt-5" id='printarea' style="min-height: 600px;">
        <div class="card-body">

            <div class="row">
            <div class="col-md-5">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-md-8 p-0 mt-2 pading5px">
                            <div class="input-group input-group-alt profession-slect">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Tag Name</button>
                                </div>
                                <asp:TextBox ID="TxtTag" placeholder="Enter Tag Name" runat="server" CssClass="form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ValidationError" ErrorMessage="*Field Is Required" Display="Dynamic" ControlToValidate="TxtTag" ForeColor="Red" ValidationGroup="savcheck"></asp:RequiredFieldValidator>

                            </div>
                        </div>

                    </div>
                           
                        <div class="form-group">
                        <asp:LinkButton ID="lnkSave" runat="server"  ValidationGroup="savcheck" OnClick="lnkSave_Click1" CssClass="btn btn-primary okBtn">Save</asp:LinkButton>

                    </div>


                </div>
            </div>


            <div class="col-md-6">
                <div class="table table-responsive">
                    <asp:GridView runat="server" ID="GvTagSetup" AutoGenerateColumns="False" CssClass="table-condensed table-hover table-bordered grvContentarea"
                      AllowPaging="true" OnPageIndexChanging="GvTagSetup_PageIndexChanging">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSlNo0" runat="server" Font-Bold="True" Height="16px"
                                        Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "." %>' Width="30px">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tagid">
                                <ItemTemplate>
                                    <asp:Label ID="lblTagid" runat="server" Style="font-size: 12px; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tagid")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>     
                            
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesciption" runat="server" Style="font-size: 12px; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tagname")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkedit" runat="server" Text="Edit" OnClick="lnkedit_Click"></asp:LinkButton>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="gvPagination" />
                    </asp:GridView>
                </div>
            </div>

            </div>

        </div>
    </div>
</asp:Content>
