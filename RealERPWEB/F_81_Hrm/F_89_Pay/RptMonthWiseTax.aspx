<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptMonthWiseTax.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.RptMonthWiseTax" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        div#ContentPlaceHolder1_ddlCompanyAgg_chzn{
        width:100%!important;
        }
        div#ContentPlaceHolder1_ddldepartmentagg_chzn{
            width:100%!important;
        }
        div#ContentPlaceHolder1_ddlProjectName_chzn{
             width:100%!important;
        }
        div#ContentPlaceHolder1_ddlEmployee_chzn{
              width:100%!important;
        }
        .mt20{
            margin-top:20px;
        }
        .chzn-drop{
            width:100%!important;
        }
                        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
                .card-body{
                    min-height:400px!important;
                }
                        .pd4{
                    padding:4px!important;
                }

    </style>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

              <%--  var gvpf = $('#<%=this.gvsalary.ClientID %>');


            gvpf.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../../Image/arrowvt.png",
                varrowbottomimg: "../../Image/arrowvb.png",
                harrowleftimg: "../../Image/arrowhl.png",
                harrowrightimg: "../../Image/arrowhr.png",
                freezesize: 10
            });--%>


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

            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-3 col-md-3  col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server">Company</asp:Label>
                                <asp:DropDownList ID="ddlCompanyAgg" runat="server" CssClass="form-control chzn-select w-100" OnSelectedIndexChanged="ddlCompanyAgg_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3  col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lbldeptnameagg" runat="server">Department</asp:Label>
                                <asp:DropDownList ID="ddldepartmentagg" runat="server" CssClass="form-control  chzn-select" TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddldepartmentagg_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblsection" runat="server">Section</asp:Label>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select" AutoPostBack="True" TabIndex="7" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblpreAdv" runat="server">Employee</asp:Label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control chzn-select" AutoPostBack="True" TabIndex="7" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-1 col-md-2 col-sm-6">
                            <asp:Label ID="lblfrmdate" runat="server">From</asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>



                        </div>

                        <div class="col-lg-1 col-md-2 col-sm-6">
                            <asp:Label ID="lbltodate" runat="server">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass=" form-control form-control-sm pd4"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-2">
                                   <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                        </div>
                    </div>

                    
                </div>
                <div class="card-body">
                                 <asp:GridView ID="gvsalary" runat="server" AutoGenerateColumns="False"
                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                    <RowStyle />
                    <Columns>


                        <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Card #">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalcardno" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                    Width="45px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <table style="width: 170px">
                                    <tr>
                                        <td class="style225">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Height="16px"
                                                Text=" Employee Name " Width="90px"></asp:Label>
                                        </td>
                                        <td class="style237">
                                            <asp:HyperLink ID="hlbtntbCdataExel11" runat="server" BackColor="#000066"
                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                ForeColor="White" Style="text-align: center" Width="80px">Export Exel</asp:HyperLink>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lgvsalempname" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                    Width="170px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="Section">
                            <ItemTemplate>
                                <asp:Label ID="lgsalsection" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                    Width="120px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="amt1">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalamt1" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvsalFamt1" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt2">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalamt2" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvsalFamt2" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt3">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalamt3" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lgvsalFamt3" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt4">
                            <ItemTemplate>
                                <asp:Label ID="lgsvsalamt4" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvsalFamt4" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt5">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalamt5" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvsalFamt5" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt6">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalamt6" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lgvsalFamt6" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt7">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalamt7" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lgvsalFamt7" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt8">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalamt8" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvsalFamt8" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt9">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalamt9" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lgvsalFamt9" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt10">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalamt10" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvsalFamt10" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt11">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalamt11" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lgvsalFamt11" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt12">
                            <ItemTemplate>
                                <asp:Label ID="lgvsalamt12" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvsalFamt12" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Tax">
                            <ItemTemplate>
                                <asp:Label ID="lgsaltoamt" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvsalFtoam" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>


                    </Columns>
                    <FooterStyle CssClass="grvFooter" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />
                </asp:GridView>
                </div>




                <%--   <div class=" table table-responsive">--%>

   


            </div>




            <script type="text/javascript" language="javascript">

                $(document).ready(function () {

                    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

                });

                function pageLoaded() {


                    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                    var gvsalary = $('#<%=this.gvsalary.ClientID %>');

                    gvsalary.Scrollable();
                }

            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

