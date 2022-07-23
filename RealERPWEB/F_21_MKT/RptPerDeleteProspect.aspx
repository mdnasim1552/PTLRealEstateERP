<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPerDeleteProspect.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptPerDeleteProspect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .chzn-container-single {
            width: 210px !important;
            height: 34px !important;
        }

            .chzn-container-single .chzn-single {
                height: 36px !important;
                line-height: 36px;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        $('.chzn-container').css('width', '250px');

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });

            $('.chzn-select').chosen({ search_contains: true });

        };
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
            <div class="card card-fluid">
                <div class="card-header mt-3">
                    <div class="row">
                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lbldate" runat="server" class="control-label" Text="From"></asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lbltodate" runat="server" class="control-label" Text="To"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" class="control-label" Text="Page Size"></asp:Label>
                                <asp:DropDownList ID="ddlpage" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
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
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lnkbtnOk_Click" CssClass="btn btn-primary btn-sm" style="margin-top: 20px;"></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body" style="min-height: 400px;">
                    <div class="row mb-3">
                        <div class="col-md-12 table-responsive">
                            <asp:GridView ID="gvPerDelProspect" runat="server" AutoGenerateColumns="False"
                                PageSize="10" AllowPaging="true" OnPageIndexChanging="gvPerDelProspect_PageIndexChanging"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL." HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" 
                                                Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Prospect Name">
                                        <HeaderTemplate>
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Prospect Name" Width="120px"></asp:Label>
                                            <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Export To Excel"><i class="fas fa-file-excel"></i>
                                            </asp:HyperLink>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProsName" runat="server"
                                                Font-Size="12px" Font-Underline="False" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prospectname")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Associate Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAssocName" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assocname")) %>'
                                                Width="120px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Create Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCreateDate" runat="server" 
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Contact No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPhone" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                Width="80px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProfession" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                                Width="120px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAddress" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                Width="150px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Source">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSource" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadsrc")) %>'
                                                Width="70px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                </Columns>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
