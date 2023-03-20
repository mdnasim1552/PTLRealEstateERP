<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpAssesmentCode.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.EmpAssesmentCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">

                                    <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                </div>
                            </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row mb-3">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewAppLetterInfo" runat="server">
                                <asp:GridView ID="grvAssessmntCodeinfo" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" CellPadding="4" Font-Size="12px"
                                    OnRowCancelingEdit="grvAssessmntCodeinfo_OnRowCancelingEdit"
                                    OnRowEditing="grvAssessmntCodeinfo_OnRowEditing"
                                    OnRowUpdating="grvAssessmntCodeinfo_OnRowUpdating" PageSize="15" ShowFooter="True"
                                    Width="915px" OnPageIndexChanging="grvAssessmntCodeinfo_OnPageIndexChanging"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea">

                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid1" runat="server" ForeColor="Black"
                                                    Style="text-align: right" Font-Size="X-Small" Font-Bold="true"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                                        </asp:TemplateField>
                                        <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                            SelectText="" ShowEditButton="True">
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                                            <ItemStyle Font-Bold="True" Font-Size="X-Small" ForeColor="#0000C0" />
                                        </asp:CommandField>
                                        <asp:TemplateField HeaderText=" ">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblasscode" runat="server" ForeColor="Black" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asscode2"))+"-" %>'
                                                    Width="20px"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" ForeColor="Black" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asscode2"))+"-" %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtasscode" runat="server" Height="16px" MaxLength="6" Font-Size="X-Small"
                                                    Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asscode3")) %>'
                                                    Width="40px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbgrketcode4" runat="server" ForeColor="Black" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asscode3")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Code">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtassDesc" runat="server" Font-Size="X-Small"                                                    
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assdesc")) %>' Width="500px"></asp:TextBox>
                                            </EditItemTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblassDesc" runat="server" ForeColor="Black"
                                                    Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assdesc")) %>'
                                                    Width="500px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Base Value">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtBaseValue" runat="server"  Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "baseval")) %>' ></asp:TextBox>
                                            </EditItemTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblBaseValue" runat="server" ForeColor="Black"
                                                    Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "baseval")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <AlternatingRowStyle BackColor="" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


