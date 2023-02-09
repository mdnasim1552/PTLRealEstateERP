<%@ Page Language="C#"  MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptLeadStatus.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptLeadStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .mt20 {
            margin-top: 20px;
        }
        .mt30 {
            margin-top: 30px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });
        };

    </script>    

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                <div class="card-body">
                    <div class="row">                        
                        <div class="col-sm-2 col-md-2  col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblFdate" runat="server">From Date</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" autocomplete="off" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="csefdate" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-1.5  col-md-1.5  col-lg-1.5 ">
                            <div class="form-group">
                                <asp:Label ID="lblTdate" runat="server">To Date</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="cetdate" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>                        
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                        <div class="col-sm-2  col-md-2  col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="search" runat="server">Search</asp:Label>
                                <asp:TextBox ID="txtVal" runat="server" CssClass="form-control form-control-sm" TextMode="Search"></asp:TextBox>                                
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <asp:LinkButton ID="SrchBtn" runat="server" OnClick="SrchBtn_Click"><i class="fas fa-search mt30"></i></asp:LinkButton>
                        </div>
                        <div class="col-sm-.5 col-md-.5 col-lg-.5">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>500</asp:ListItem>
                                    <asp:ListItem>800</asp:ListItem>
                                    <asp:ListItem>1000</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>  
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid container-data">
                <div class="card-body mb-0 pb-0" style="min-height:400px">
                    <div class="row mb-0 pb-0">
                        <div class="col-md-8">
                            <div class="row">
                                <asp:GridView ID="gvLeadStatus" runat="server" AutoGenerateColumns="False"
                                    PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLeadStatus_PageIndexChanging"
                                    ShowFooter="True" CssClass=" table-striped table-bordered grvContentarea"
                                    HeaderStyle-Font-Size="14px" Width="800px">
                                    <RowStyle />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <Columns>                                       
                                        <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" 
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P-ID" HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvproscod1" runat="server" Height="16px" 
                                                    Style="text-align: center"
                                                    Text='<%# "P-" + Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod1")) %>' Width="100px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Associate Name" HeaderStyle-Width="110px" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvteamleader" runat="server" Height="16px" 
                                                    Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamleader")) %>'  Width="120px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Source">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsource" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "source")) %>'
                                                    Width="110px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Generate Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIntlostdate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="90px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvquerydate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "query")) %>'
                                                    Width="90px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lead">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleaddate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lead")) %>'
                                                    Width="90px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qualified Lead">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqualifiedleaddate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qualifiedlead")) %>'
                                                    Width="90px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Negotiation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnegotiationdate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "negotiation")) %>'
                                                    Width="90px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Final Negotiation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvfinalnegotiationdate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "finalnegotiation")) %>'
                                                    Width="90px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Win">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvwindate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "win")) %>'
                                                    Width="90px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlstatus" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'
                                                    Width="90px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>