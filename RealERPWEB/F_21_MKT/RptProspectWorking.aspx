<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptProspectWorking.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptProspectWorking" %>

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
        /* .profession-slect .chzn-container-single {
            width: 180px !important;
            height: 34px !important;
        }
        .grvContentarea {
        }

        .srDiv .chzn-container-single {
            width: 155px !important;
        }*/
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
            <div class="card card-fluid container-data mt-4" style="min-height: 550px;">
                <div class="card-header mt-3">
                    <div class="row mb-3" id="divFilter">
                        <div class="col-md-2 col-lg-2 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <button class="btn btn-secondary" type="button">From Date</button>
                                </div>
                                <asp:TextBox ID="txtFrmDate" ClientIDMode="Static" AutoCompleteType="Disabled" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1_txtFrmDate" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFrmDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                         <div class="col-md-2 col-lg-2 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <button class="btn btn-secondary ml-1" type="button">To Date</button>
                                </div>
                                <asp:TextBox ID="txtToDate" ClientIDMode="Static" AutoCompleteType="Disabled" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1_txtToDate" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtToDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-3 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Team Lead</button>
                                </div>
                                <asp:DropDownList ID="ddlEmpid" ClientIDMode="Static" data-placeholder="Choose Employee.." runat="server" CssClass="custom-select chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Page</button>
                                </div>
                                <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>400</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 p-0 ml-2">
                            <asp:LinkButton ID="lnkbtnOk" runat="server" Text="Ok" CssClass="btn btn-primary okBtn" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row mb-3">
                        <div class="col-md-12 table-responsive">
                            <asp:GridView ID="gvProspectWorking" runat="server" AutoGenerateColumns="False"
                                PageSize="10" AllowPaging="true" OnPageIndexChanging="gvProspectWorking_PageIndexChanging"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                HeaderStyle-Font-Size="14px" Width="800px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL." HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDescrption" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                                Width="120px" ForeColor="Black" Font-Bold="true" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField
                                        HeaderText="Prospect Name">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblProsName" runat="server" Font-Bold="True"
                                                Text="Prospect Name" Width="120px"></asp:Label>
                                            <asp:HyperLink ID="hlnkbtnProsWorking" runat="server"
                                                CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProsName" runat="server"
                                                Font-Size="12px" Font-Underline="False" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prospectname")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Generate Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvGenerateDate" runat="server" Height="16px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generatedate")).ToString("dd-MMM-yyyy") %>'
                                                Width="90px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Followup Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvFollowupDate" runat="server" Height="16px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "followupdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="90px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Contact No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPhone" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                Width="90px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Profession">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProfession" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'
                                                Width="80px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAddress" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                Width="100px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Interested Project">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvIntProject" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "interestproj")) %>'
                                                Width="120px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Source">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSource" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadsrc")) %>'
                                                Width="70px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Discussion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDiscussion" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ldiscuss")) %>'
                                                Width="320px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
