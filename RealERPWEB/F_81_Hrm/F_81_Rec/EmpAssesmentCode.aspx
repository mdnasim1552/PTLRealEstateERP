<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpAssesmentCode.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.EmpAssesmentCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container contentPart">
                <div class="row">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">

                                <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                            </div>
                        </div>
                </div>
                <div class="row">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewAppLetterInfo" runat="server">
                            <asp:GridView ID="grvAssessmntCodeinfo" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" CellPadding="4" Font-Size="12px"
                                OnRowCancelingEdit="grvAssessmntCodeinfo_OnRowCancelingEdit"
                                OnRowEditing="grvAssessmntCodeinfo_OnRowEditing"
                                OnRowUpdating="grvAssessmntCodeinfo_OnRowUpdating" PageSize="15" ShowFooter="True"
                                Width="915px" OnPageIndexChanging="grvAssessmntCodeinfo_OnPageIndexChanging">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid1" runat="server" ForeColor="Black"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                        SelectText="" ShowEditButton="True">
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText=" ">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblasscode" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asscode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asscode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtasscode" runat="server" Height="16px" MaxLength="6"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asscode3")) %>'
                                                Width="40px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrketcode4" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asscode3")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtassDesc" runat="server" Height="80px"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assdesc")) %>'
                                                TextMode="MultiLine" Width="750px"></asp:TextBox>
                                        </EditItemTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblassDesc" runat="server" ForeColor="Black"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assdesc")) %>'
                                                Width="750px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


