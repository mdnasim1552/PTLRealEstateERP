<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpInvestmentPlan.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpInvestmentPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gvColvsExp = $('#<%=this.gvColvsExp.ClientID %>');
            gvColvsExp.Scrollable();

        }

    </script>






    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewMasterBgd" runat="server">
                            <fieldset>
                                <div class="form-group">
                                    <div class="col-md-3 asitCol2 pading5px">
                                        <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName"
                                            Text="Project Name:"></asp:Label>

                                        <asp:Label ID="lblmaxproject" runat="server"
                                            Font-Bold="True" Font-Size="12px" ForeColor="Black" CssClass="lblTxt lblName"
                                            oncheckedchanged="chkall_CheckedChanged" Text="Select Maximum Twelve Project" />

                                        <asp:CheckBox ID="chkDeselectAll" runat="server" AutoPostBack="True" CssClass="checkbox"
                                            OnCheckedChanged="chkDeselectAll_CheckedChanged" Text=" Uncheck All" />
                                    </div>

                                    <div class="clearfix"></div>
                                </div>

                                <div class="form-group">


                                    <asp:CheckBoxList ID="cblProject" runat="server" CssClass=" chkBoxControl" Font-Bold="True"
                                        RepeatColumns="1" RepeatDirection="Vertical">
                                        <asp:ListItem>aa</asp:ListItem>
                                        <asp:ListItem>bb</asp:ListItem>
                                        <asp:ListItem>cc</asp:ListItem>
                                    </asp:CheckBoxList>


                                    <asp:LinkButton ID="lbtnShow" runat="server"
                                        OnClick="lbtnShow_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>

                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                        <ProgressTemplate>
                                            <asp:Label ID="Label3" runat="server" CssClass="lblProgressBar"
                                                Text="Please wait . . . . . . ." Width="120px"></asp:Label>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <div class="clearfix"></div>
                                </div>

                            </fieldset>
                            <asp:GridView ID="gvBgd" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvBgd_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                           "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtopamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P1">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P2">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P3">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P4">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P5">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P6">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P7">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P8">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P9">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p9")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P10">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P11">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p11")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P12">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp12" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p12")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>




                        <asp:View ID="ViewSourcees" runat="server">
                            <fieldset>
                                <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblDaterange" runat="server" CssClass="lblTxt lblName" Text="From:"></asp:Label>

                                        <asp:TextBox ID="txtDatefrom" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>

                                        <asp:Label ID="lblDateto" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>

                                        <asp:TextBox ID="txtDateto" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>

                                        <asp:Label ID="lblreportlevel" runat="server" CssClass="smLbl_to" Text="Report Level:"></asp:Label>

                                        <asp:DropDownList ID="ddlReportLevel" runat="server" CssClass=" ddlistPull">
                                            <asp:ListItem Value="2">Main</asp:ListItem>
                                            <asp:ListItem Value="4">Sub-1</asp:ListItem>
                                            <asp:ListItem Value="8">Sub-2</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:LinkButton ID="lbtnShowSource" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lbtnShowSource_Click">Ok</asp:LinkButton>

                                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="table table-responsive">

                                <asp:GridView ID="gvSource" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSource_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText=" Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rptdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br><br>" : "") + 
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")).Trim(): "") %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="As Of Today">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtoamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamtup" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amtup")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total.Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamttoamtwp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamtwp")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="amt1">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="amt2">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="amt3">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="amt4">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="amt5">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="amt6">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="amt7">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="amt8">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="amt9">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="amt10">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="amt11">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="amt12">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt12" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>

                        </asp:View>

                        <asp:View ID="View2" runat="server">
                            <div class="table table-responsive">
                                <div class="form-group">
                                    <div class="col-md-3 asitCol2 pading5px">
                                        <asp:Label ID="lblProject" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>

                                    <div class="col-md-3 asitCol3">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass=" ddlPage">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server" QueryPattern="Contains"
                                            TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>
                                    </div>

                                    <div class="col-md-3 asitCol2 pading5px">
                                        <asp:LinkButton ID="lbtnShowPDetails" runat="server"
                                            CssClass="btn btn-primary primaryBtn"
                                            OnClick="lbtnShowPDetails_Click">Ok</asp:LinkButton>

                                        <asp:Label ID="lblmsg01" runat="server"></asp:Label>
                                    </div>

                                    <div class="clearfix"></div>

                                </div>

                                <div class="form-group">

                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblDaterange0" runat="server" CssClass="lblTxt lblName" Text="From:"></asp:Label>

                                        <asp:TextBox ID="txtDatefromd" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatefromd_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDatefromd"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3 asitCol2 pading5px">
                                        <asp:Label ID="lblDateto0" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>

                                        <asp:TextBox ID="txtDatetod" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatetod_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDatetod"></cc1:CalendarExtender>
                                    </div>


                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlReportLeveld" runat="server" CssClass=" ddlistPull">
                                            <asp:ListItem Value="2">Main</asp:ListItem>
                                            <asp:ListItem Value="4">Sub-1</asp:ListItem>
                                            <asp:ListItem Value="7">Sub-2</asp:ListItem>
                                            <asp:ListItem Value="9">Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlReportLevelA" runat="server" CssClass="ddlistPull">
                                            <asp:ListItem Value="2">Main</asp:ListItem>
                                            <asp:ListItem Value="4">Sub-1</asp:ListItem>
                                            <asp:ListItem Value="8">Sub-2</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>





                                </div>
                            </div>
                            <asp:GridView ID="gvProDetials" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvProDetials_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvResDescd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="As Of Today">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtoamtd" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtupd" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amtup")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt1">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt1d" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt2">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt2d" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt3">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt3d" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt4">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt4d" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt5">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt5d" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt6">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt6d" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt7">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt7d" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt8">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt8d" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt9">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt9d" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt10">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt10d" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt11">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt11d" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amt12">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt12d" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>
                        <asp:View ID="VBgdVsExpenses" runat="server">


                            <div class="form-group">
                                <div class="col-md-3 asitCol3 pading5px">
                                    <asp:Label ID="lblDaterange1" runat="server" CssClass="lblTxt" Text="Date:"></asp:Label>

                                    <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                                    <asp:LinkButton ID="lbtnProVsExp" runat="server"
                                        CssClass="btn btn-primary primaryBtn"
                                        OnClick="lbtnProVsExp_Click">Ok</asp:LinkButton>
                                </div>


                                <div class="clearfix"></div>
                            </div>
                            <asp:GridView ID="gvBgdVsExp" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Description" FooterText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvResDescp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="Black" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted Cost">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbgdamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbgdcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvaccost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFacamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remaining Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFreamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Progress(%)">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvprogress" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propercnt")).ToString("#,##0.00;(#,##0.00); ")+"%" %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remaining Cost(%)">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvperrcsot" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ")+"%" %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>
                        <asp:View ID="SalesVsCollection" runat="server">

                            <div class="row">
                                <div class="form-group">
                                    <div class="col-md3 asitCol3">
                                        <asp:Label ID="lblDaterange2" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>

                                        <asp:TextBox ID="txtDateCollect" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender0" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateCollect"></cc1:CalendarExtender>

                                        <asp:LinkButton ID="lbtnCollectVsExpenses" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lbtnCollectVsExpenses_Click">Ok</asp:LinkButton>
                                    </div>

                                    <div class="clearfix"></div>
                                </div>

                            </div>

                            <asp:GridView ID="gvSalVsCollect" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField FooterText="Total" HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRptDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="Black" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Sales">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFSalVal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbgdbgdamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sold Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvacSoldval" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFSalAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unsold Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvColAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usolamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFUsolAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Collection">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvprogress0" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFColAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remaining Collection">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRemCol" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcolamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFrColAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remaining Fund">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvperrcol" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rfundamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFrFundAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>
                        <asp:View ID="ViewComProCost" runat="server">
                            <div class="form-group">
                                <div class="col-md-4  pading5px">
                                    <asp:Label ID="lblProject1" runat="server" CssClass="lblTxt lblName" Text="Details:"></asp:Label>

                                    <asp:DropDownList ID="ddlDetailsCode" runat="server" CssClass="ddlPage" Width="238px">
                                    </asp:DropDownList>


                                    <asp:LinkButton ID="lbtnShowComCost" runat="server" CssClass="btn btn-primary primaryBtn"
                                        OnClick="lbtnShowComCost_Click">Ok</asp:LinkButton>
                                </div>

                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3  pading5px">
                                    <asp:Label ID="lblDaterange3" runat="server" CssClass="smLbl_to" Text="From:"></asp:Label>

                                    <asp:TextBox ID="txtDatefromCom" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDatefromCom_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDatefromCom"></cc1:CalendarExtender>
                                    <cc1:CalendarExtender ID="txtDatefromd_CalendarExtender0" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDatefromd"></cc1:CalendarExtender>


                                    <asp:Label ID="lblDateto1" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>

                                    <asp:TextBox ID="txtDatetoCom" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDatefromCom"></cc1:CalendarExtender>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDatefromd"></cc1:CalendarExtender>

                                </div>

                                <div class="col-md-3">
                                    <asp:Label ID="lblreportlevel1" runat="server" CssClass="smLbl_to" Text="Report Level:"></asp:Label>

                                    <asp:DropDownList ID="ddlReportLevelCom" runat="server" CssClass="ddlPage">
                                        <asp:ListItem Value="2">Main</asp:ListItem>
                                        <asp:ListItem Value="4">Sub-1</asp:ListItem>
                                        <asp:ListItem Value="7">Sub-2</asp:ListItem>
                                        <asp:ListItem Value="9">Sub-3</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                                    </asp:DropDownList>
                                </div>


                                <div class="clearfix"></div>
                            </div>
                            <div class="table table-responsive">
                                <asp:GridView ID="gvComCost" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField FooterText="Total" HeaderText=" Description">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Height="16px" Text="Description"
                                                                Width="180px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnCdataExel" runat="server" CssClass="btn btn-warning primaryBtn">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvActDescCost" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoCost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtopamtcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P1">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P2">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P3">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P4">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P5">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P6">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P7">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P8">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p8")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P9">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p9")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P10">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p10")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P11">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p11")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P12">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc12" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p12")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P13">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc13" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p13")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC13" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P14">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc14" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p14")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC14" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P15">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc15" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p15")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC15" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P16">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc16" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p16")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC16" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P17">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc17" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p17")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC17" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P18">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc18" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p18")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC18" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P19">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc19" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p19")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC19" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P20">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc20" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p20")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC20" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P21">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc21" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p21")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC21" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P22">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp22" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p22")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC22" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P23">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc23" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p23")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC23" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P24">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc24" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p24")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC24" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P25">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc25" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p25")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC25" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P26">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc26" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p26")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC26" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P27">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc27" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p27")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC27" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P28">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc28" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p28")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC28" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P29">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc29" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p29")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC29" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P30">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc30" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p30")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC30" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P31">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc31" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p31")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC31" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P32">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc32" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p32")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC32" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P33">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc33" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p33")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC33" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P34">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc34" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p34")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC34" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P35">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc35" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p35")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC35" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P36">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc36" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p36")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC36" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P37">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc37" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p37")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC37" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P38">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc38" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p38")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC38" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P39">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc39" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p39")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC39" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P40">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc40" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p40")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC40" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P41">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc41" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p41")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC41" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P42">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc42" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p42")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC42" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P43">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc43" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p43")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC43" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P44">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc44" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p44")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC44" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P45">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc45" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p45")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC45" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P46">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc46" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p46")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC46" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P47">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc47" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p47")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC47" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P48">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc48" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p48")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC48" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P49">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc49" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p49")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC49" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P50">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc50" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p50")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC50" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P51">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC51" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc51" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p51")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P52">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc52" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p52")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC52" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P53">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc53" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p53")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC53" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P54">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc54" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p54")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC54" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P55">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc55" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p55")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC55" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P56">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc56" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p56")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC56" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P57">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc57" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p57")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC57" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P58">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc58" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p58")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC58" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P59">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc59" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p59")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC59" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P60">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc60" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p60")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC60" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P61">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc61" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p61")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC61" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P62">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc62" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p62")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC62" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P63">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc63" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p63")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC63" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P64">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc14" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p64")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC64" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P65">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc65" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p65")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC65" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P66">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc66" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p66")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC66" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P67">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc67" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p67")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC67" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P68">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc68" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p68")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC68" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P69">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc69" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p69")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC69" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P70">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc70" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p70")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC70" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P71">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc71" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p71")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC71" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P72">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp22" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p72")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC72" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P73">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc73" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p73")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC73" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P74">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc74" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p74")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC74" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P75">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc75" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p75")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC75" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P76">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc76" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p76")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC76" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P77">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc77" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p77")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC77" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P78">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc78" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p78")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC78" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P79">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc79" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p79")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC79" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P80">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc80" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p80")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC80" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P81">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc81" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p81")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC81" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P82">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc82" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p82")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC82" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P83">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc83" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p83")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC83" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P84">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc84" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p84")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC84" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P85">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc85" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p85")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC85" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P86">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc86" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p86")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC86" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P87">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc87" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p87")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC87" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P88">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc88" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p88")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC88" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P89">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc89" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p89")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC89" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P90">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc90" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p90")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC90" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P91">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc91" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p91")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC91" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P92">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc92" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p92")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC92" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P93">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc93" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p93")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC93" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P94">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc94" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p94")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC94" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P95">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc95" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p95")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC95" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P96">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc96" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p96")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC96" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P97">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc97" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p97")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC97" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P98">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc98" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p98")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC98" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P99">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc99" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p99")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC99" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P100">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpc100" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p100")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPC100" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>


                        </asp:View>
                        <asp:View ID="CollectVsExpenses" runat="server">
                            <div class="form-group">
                                <div class="col-md-3  pading5px">
                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>

                                    <asp:TextBox ID="lbldate" runat="server" Width="80px" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd-MMM-yyyy"
                                        TargetControlID="lbldate"></cc1:CalendarExtender>

                                    <asp:LinkButton ID="lbtnShowColvsExp" runat="server" CssClass="btn btn-primary primaryBtn"
                                        OnClick="lbtnShowColvsExp_Click">Ok</asp:LinkButton>
                                </div>
                                <div class="col-md-5 pading5px">
                                    <asp:CheckBox ID="chkconsolidate" runat="server" CssClass=" chkBoxControl"
                                        Text="Consolidate" />

                                    <asp:Label ID="Label13" runat="server" Text="Taka in Crore" CssClass="smLbl_to"></asp:Label>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="table table-responsive">

                                <asp:GridView ID="gvColvsExp" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvColvsExp_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total" HeaderText="Description">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Height="16px" Text="Description"
                                                                Width="70px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnCdataExelExp" runat="server" CssClass="btn btn-warning primaryBtn">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvIActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "")  %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="Black" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="Loc" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjloc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Area">
                                            <ItemTemplate>
                                                <asp:Label ID="lanarea" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lanarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nature of Land">
                                            <ItemTemplate>
                                                <asp:Label ID="natland" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "natland")) %>'
                                                    Width="45px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Revenue <br/> A">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtosales" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="45px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFToSalVal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="45px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales <br/> B">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsales" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFSaleAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="40px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collection <br/> C">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcoll" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCollAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales in % <br/> D=B/A">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvperonsale" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontosal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFperonsale" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="40px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collection in % <br/> E=C/A">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvperontocol" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontocol")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFperontocol" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Cost <br/> F">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBgdAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBgdAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Cost <br/> G">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFExpAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvExp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "examt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost in % <br/> H=G/F">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvperonPro" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronpgres")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFperonPro" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="40px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Investment Position <br/> I=H-E">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvipamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ipamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFipamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remaining Collection <br/> From Sold <br/> J=B-C">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrecoll" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsalamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFreCollAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remaining Collection <br/> From Sold & Un-sold <br/> K=A-C">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusoamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usoamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFusoamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remaining Cost <br/> L=F-G">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFRbgdAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrebgdamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rbgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fund to be generated <br/> From Sold <br/> M=J-L">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfsamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fsamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFfsamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fund to be generated <br/> From Sold & Un-sold <br/> N=K-L">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfsuamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fsuamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFfsuamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Investment  <br/> Required <br/> O=FxI<0">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFresdramt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvresdramt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invreqamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Investment <br/> Blocked <br/>  P=FxI>0">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFrescramt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrescramt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invblkamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Liabilities <br/> Q">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFlibamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlibamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "libamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NOI <br/> R">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFnoiamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnoiamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noiamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan To H/O <br/> S=((C-G)+R)>0">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFltoamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvltoamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltoamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan From H/O <br/> T=((C-G)+R)<0">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFlfrmamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlfrmamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lfrmamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Sales <br/> U=AxH%">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbgdsaamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbgdsaamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdsaamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Profit <br/> V=(A-F)/<br/>FxG+R">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFnp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "np")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Salable (SFT)">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtsalablearea" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsalablearea" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalable")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Sold (SFT)">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsoldarea" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsoldarea" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsold")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Unsold Sold (SFT)">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFusoldarea" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusoldarea" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unsold")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Construction Area in SFT">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFconsarea" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvconsarea" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Catagory">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcatagory" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagory"))%>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Const. Progress in %">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvconprogress" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conprogress")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Completion Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcompletiondate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comdate"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month Remaining">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmonremain" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mremain")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
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

                                <div class=" clearfix"></div>
                            </div>

                            <asp:Label ID="lblBankstatus" runat="server" CssClass="lblTxt lblName"
                                Text="Note:"></asp:Label>
                            <div class="clearfix"></div>


                            <asp:GridView ID="gvCatdesc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Catagory">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcatdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagory")) 
                                                                        %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="Black" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdetcatdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagorydesc")) 
                                                                        %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="Black" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                            <div class="form-group">
                                <div class="col-md-12  pading5px">
                                    <asp:Label ID="lblColl" runat="server" CssClass=" smLbl_to"
                                        Text="A. COLLECTION % OF SALES (C/B x 100)"></asp:Label>

                                    <asp:Label ID="txtColl" runat="server" CssClass="smLbl_to" Text="Collection %"></asp:Label>

                                    <asp:Label ID="lblCost" runat="server" Font-Bold="True" CssClass="smLbl_to" Text="B. NP % OF COST (V/G x 100)"></asp:Label>

                                    <asp:Label ID="txtNp" runat="server" Font-Bold="True" CssClass="smLbl_to" Text="NP %"></asp:Label>

                                    <asp:Label ID="lblBgd" runat="server" Font-Bold="True" CssClass="smLbl_to" Text="C. ACTUAL SALES % OF BUDGETED SALES (B/U x 100)"></asp:Label>

                                    <asp:Label ID="txtBgd" runat="server" Font-Bold="True" CssClass="smLbl_to" Text="Bgd %"></asp:Label>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="clearfix"></div>





                        </asp:View>
                        <asp:View ID="CostOfFund" runat="server">
                            <div class="form-group">
                                <div class="col-md-3 asitCol3 pading5px">
                                    <asp:Label ID="lblDate0" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>

                                    <asp:TextBox ID="txtDateCostofFund" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd-MMM-yyyy"
                                        TargetControlID="txtDateCostofFund"></cc1:CalendarExtender>
                                </div>

                                <div class="col-md-8  pading5px">
                                    <asp:Label ID="LblIntrstRate" runat="server" CssClass="smLbl_to" Text="Interest Rate(Per Month)" Width="150"></asp:Label>

                                    <asp:TextBox ID="TxtIntrstRate" runat="server" CssClass="inputtextbox">1.25%</asp:TextBox>

                                    <asp:LinkButton ID="LinkButtonShowCostOfFund" runat="server" CssClass="btn btn-primary okBtn"
                                        OnClick="LinkButtonShowCostOfFund_Click">Ok</asp:LinkButton>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="table table-responsive">


                                <asp:GridView ID="gvCostOfFund" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvCostOfFund_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total" HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "")  %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="Black" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collection">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcoll" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCollAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expenses">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFExpAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvExp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "examt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Finance">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDiffAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diff")).ToString("#,##0;(#,##0);") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDiffAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Interest">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcostfund" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costfund")).ToString("#,##0;(#,##0);")%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcostfund" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>

                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

