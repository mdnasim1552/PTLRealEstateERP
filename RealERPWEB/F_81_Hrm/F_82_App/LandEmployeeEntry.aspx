<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="LandEmployeeEntry.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.LandEmployeeEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

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
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 550px;">
                    <div class="card card-fluid">
                        <div class="card-body mb-3">
                            <div class="row">
                                <div class="col-sm-1 col-md-1 col-lg-1">
                                    <asp:Label ID="lblEmployee" runat="server" Text="Employee"></asp:Label>
                                </div>
                                <div class="col-sm-4 col-md-4 col-lg-4">
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                                </div>
                                <div class="col-sm-1 col-md-1 col-lg-1">
                                    <asp:LinkButton ID="lnkBtnOk" runat="server" CssClass="btn btn-primary btn-sm ml-2" OnClick="lnkBtnOk_Click">Add</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:GridView ID="gvEmpSalLand" runat="server" CssClass="table-striped table-bordered grvContentarea" AutoGenerateColumns="False" ShowFooter="True" Width="900px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btndelete" Width="20px" OnClick="btndelete_Click" OnClientClick="return confirm('Are you want to delete?')"><i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblempid" runat="server" Font-Bold="true" Font-Size="12px" Visible="False"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                        Width="160px"></asp:Label>
                                    <asp:Label ID="lblgvempname" runat="server" Font-Bold="true" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                        Width="160px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesig" runat="server"
                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                        Width="190px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbntUpdateOtherDed" runat="server" CssClass="btn btn-success successBtn" OnClick="lbntUpdateOtherDed_Click" >Update</asp:LinkButton>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSection" runat="server" Font-Bold="true" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Card No">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCardno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Bold="True" Font-Size="16px" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
