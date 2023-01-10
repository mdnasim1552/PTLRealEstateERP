<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPurchesStatusPrjWise.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPurchesStatusPrjWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            //$(".pop").on("click", function () {
            //    $('#imagepreview').attr('src', $(this).attr('src')); // here asign the image to the modal when the user click the enlarge link
            //    $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
            //});
            $('.chzn-select').chosen({ search_contains: true });

        }

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
            <div class="card mt-3">
                <div class="card-header bg-light">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">From</asp:Label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label runat="server" ID="text">Project Name</asp:Label>
                                <asp:DropDownList ID="ddlprojectname" runat="server" AutoPostBack="true" CssClass="chzn-select form-control form-control-sm "></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mt-4">
                            <div class="form-group-">
                                <asp:LinkButton ID="lnkbtnok" runat="server" OnClick="lnkbtnok_Click" CssClass=" btn btn-primary btn-sm mt20">Ok</asp:LinkButton></li>
                            </div>
                        </div>
                    </div>
                    <div class="row " runat="server" ID="Pagging">
                         <div class="col-md-2">
                                        <asp:Label ID="lblPage" runat="server">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="divPnl">
                         <asp:Panel ID="pnlAllProjectreport" runat="server" Visible="false">
                    <div class="table-responsive mt-2">
                        <asp:GridView ID="gv_PurchesSummary" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnRowDataBound="gv_PurchesSummary_RowDataBound" OnPageIndexChanging="gv_PurchesSummary_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="SL # ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right; font-size: 16px;"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ProjectName">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" ID="lnkbtndetailslink" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2">
                                        <asp:Label ID="lblprjnamebatchwise" runat="server" Width="300px"
                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                            ForeColor="Black"></asp:Label>
                                            </asp:HyperLink>
                                        <asp:Label ID="lablpactcode" runat="server" Width="300px" Visible="false"
                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode"))%>'
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="fgvsup" runat="server" CssClass="text-right" Font-Bold="True" Font-Size="16px"
                                            ForeColor="Black" Style="text-align: right">Total :</asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprjnamebatchwiseamt" runat="server" Width="200px"
                                            Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="tblsumAmount" runat="server" Width="100px" ForeColor="Black" Font-Bold="True" Font-Size="16px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Percentage (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpercntage" runat="server" Width="80px"
                                            Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntage")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>                         
                                     <FooterTemplate>
                                        <asp:Label ID="tblAmountper" runat="server" Width="100px" ForeColor="Black" Font-Bold="True" Font-Size="16px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="gvPagination" />

                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                             </asp:Panel>
                              <asp:Panel ID="pnlprojectDetails" runat="server" Visible="false">
                                   <div class="table-responsive mt-2">
                                   <asp:GridView ID="gvPurSum" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                     ShowFooter="True"  CssClass="table-striped table-hover table-bordered grvContentarea">
                                    
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvprojectdesc0" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty0" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate0" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt0" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmtS" runat="server" Font-Bold="True" Font-Size="16px" ForeColor="#000"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                                       </div>
                              </asp:Panel>
                        </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
