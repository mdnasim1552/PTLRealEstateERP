<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPrjFloorWise.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.RptPrjFloorWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
   
       <script src="../../../Scripts/gridviewScrollHaVertworow.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            try {

                var gridViewScroll = new GridViewScroll({
                    elementID: "gvPrjflr",
                    width: 1200,
                    height: 500,
                    freezeColumn: true,
                    freezeFooter: true,
                    freezeColumnCssClass: "GridViewScrollItemFreeze",
                    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                    freezeHeaderRowCount: 1,
                    freezeColumnCount: 8,

                });

                //var gridViewScroll = new GridViewScroll({
                //    elementID: "gvBonus",
                //    width: 1000,
                //    height: 500,
                //    freezeColumn: true,
                //    freezeFooter: true,
                //    freezeColumnCssClass: "GridViewScrollItemFreeze",
                //    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                //    freezeHeaderRowCount: 1,
                //    freezeColumnCount: 8,

                //});
                gridViewScroll.enhance();
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e) {
                alert(e);
            }
        }
    </script>
    
    <style>
        .GridViewScrollHeader TH, .GridViewScrollHeader TD {
            font-weight: normal;
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #999999;
            text-align: left;
            vertical-align: bottom;
        }

        .GridViewScrollItem TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FFFFFF;
            color: #444444;
        }

        .GridViewScrollItemFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FAFAFA;
            color: #444444;
        }

        .GridViewScrollFooterFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-top: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #444444;
        }

        .grvHeader {
            height: 38px !important;
        }

        .WrpTxt {
            white-space: normal !important;
            word-break: break-word !important;
        }
                       .mt20 {
            margin-top: 20px;
        }
                       .table th, .table td{
                           padding:2px;
                       }

    </style>
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
            <div class="card mt-4">
                <div class="card-body">
                    <div class="row mb-4">
                     
                                    <div class="col-md-3 asitCol3 pading5px d-none">
                                        
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"
                                            TabIndex="3"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                   <div class="col-md-3">
                                       <asp:Label ID="Label1" runat="server" CssClass="form-label"
                                            Text="Project Name:"></asp:Label>

                                        <asp:DropDownList ID="ddlProjectName" runat="server"
                                            CssClass="chzn-select form-control form-control-sm"    AutoPostBack="True">
                                        </asp:DropDownList>

                                       </div>

                                      <div class="col-md-1 ml-2" style="margin-top:22px;">

                                           <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-sm btn-primary"
                                            TabIndex="6">Ok</asp:LinkButton>

                                          </div>

                                   
                                    <div class="col-md-1">
                                        <asp:Label ID="lblPage" runat="server" CssClass="form-label" Font-Bold="True"
                                            Text="Size:"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged1" TabIndex="5">
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

                     <div class="card" style="min-height:480px;">
                <div class="card-body">
                    <div class="row">                     

                                         
                                 
                   
                                <asp:GridView ID="gvPrjflr" runat="server" ClientIDMode="Static" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnPageIndexChanging="gvPrjflr_PageIndexChanging1" AllowPaging="true">
                                    <PagerSettings Position="bottom" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                       

                                        <asp:TemplateField HeaderText="Resource Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvresdesc" runat="server" Font="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                      

                                        <asp:TemplateField HeaderText="Unit">

                                            <FooterTemplate>
                                                <asp:Label ID="gvFUnit" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"> Total :</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvunit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="1st">
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement1" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbasement" runat="server" Font="8px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="2nd">
                                            <ItemTemplate>
                                                <asp:Label ID="lggflr" runat="server" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement2" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="3rd">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvffloor" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement3" runat="server" Font-Bold="True" Font-Size="11px" 
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="4th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsflr" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement4" runat="server" style="text-align:right" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Width="90px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="5th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvthridflr" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement5" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="6th ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvforflr" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty6")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement6" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="7th ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfifthflr" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                           <FooterTemplate>
                                                <asp:Label ID="gvFbasement7" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                             <asp:TemplateField HeaderText="8th ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsixflr" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                           <FooterTemplate>
                                                <asp:Label ID="gvFbasement8" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                          <asp:TemplateField HeaderText="9th ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsevenflr" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty9")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement9" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="10th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgveightflr" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement10" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        
                                        <asp:TemplateField HeaderText="11th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnightflr" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty11")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement11" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        
                                        <asp:TemplateField HeaderText="12th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcommon" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty12")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                           <FooterTemplate>
                                                <asp:Label ID="gvFbasement12" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="13th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv13th" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty13")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement13" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="14th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv14th" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty14")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement14" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="15th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv15th" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty15")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement15" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="16th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv16th" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty16")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement16" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="17th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv17th" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty17")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement17" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="18th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv18th" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty18")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement18" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        
                                        <asp:TemplateField HeaderText="19th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv19th" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty19")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement19" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="20th">
                                            <ItemTemplate>
                                                <asp:Label ID="lgv20th" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty20")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvFbasement20" runat="server" Font-Bold="True" Font-Size="11px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Total Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotal" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtqty" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                         
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                           <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotalamt" runat="server" Style="text-align: right" Font="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtotal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
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

