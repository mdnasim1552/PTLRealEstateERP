<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ProjectFileDetailsEntry.aspx.cs" Inherits="RealERPWEB.F_22_Sal.ProjectFileDetailsEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .primaryBtnUp {
            height: 2rem;
            font-size: .875rem;
        }
        .mt20{
            margin-top:20px;
        }
        .chzn-container-single .chzn-single {
            height: 29px !important;
            line-height: 29px !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <div class="card card-fluid" style="min-height: 500px;">
                <div class="card-body">
                    <div class="card card-fluid">
                        <div class="card-body pl-0">
                            <div class="row mb-2">
                                <div class="col-sm-3 col-md-3 col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblProjCode" CssClass="lblTxt lblName" runat="server" Text="Project Code" ></asp:Label>
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control form-control-sm chzn-select" Width="300px"></asp:DropDownList>
                                    </div>                                    
                                </div>
                                <div class="col-sm-1 col-md-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>
                                    </div>
                                <div class="col-sm-1 col-md-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page Size"></asp:Label>
                                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlAdd" runat="server" Visible="false">
                        <div class="card card-fluid">
                            <div class="card-body pl-0">
                                <div class="row mb-2">
                                    <div class="col-sm-2 col-md-2 col-lg-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblFileNo"  CssClass="lblTxt lblName" runat="server" Text="File No"></asp:Label>
                                            <asp:TextBox ID="txtFileNo"  CssClass="form-control form-control-sm " runat="server"></asp:TextBox>
                                        </div>                                        
                                    </div>
                                    <div class="col-sm-2 col-md-2 col-lg-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblLocation"  CssClass="lblTxt lblName" runat="server" Text="File Location"></asp:Label>
                                            <asp:TextBox ID="txtLocation" CssClass="form-control form-control-sm "  runat="server" ></asp:TextBox>
                                        </div>                                        
                                    </div>
                                    <div class="col-sm-1 col-md-1 col-lg-1">
                                        <div class="form-group">
                                             <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnAdd_Click">Add</asp:LinkButton>
                                        </div>                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlGrid" runat="server" Visible="false">
                        <asp:GridView ID="gvProjFileDet" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True" Width="16px" AllowPaging="True" PageSize="2" OnPageIndexChanging="gvProjFileDet_PageIndexChanging">
                            <PagerSettings Visible="True" Mode="NumericFirstLast" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProjCode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtngvDelete" runat="server" Font-Bold="True" CssClass=" btn btn-xs" OnClick="lbtngvDelete_Click"><i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProjDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="350px"></asp:Label>
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" CssClass=" btn  btn-danger primaryBtnUp" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>--%>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvFileNo" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fileno")) %>'
                                            Width="120px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Location">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvLocation" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location")) %>'
                                            Width="150px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
