<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptCallCenterLead.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptCallCenterLead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });
        });
        function pageLoaded() {


            var gvCallCenter = $('#<%=this.gvCallCenter.ClientID %>');

            gvCallCenter.Scrollable();
            $('.chzn-select').chosen({ search_contains: true });


        }

    </script>
    <style>
        .mt20 {
            margin-top: 20px;
        }

        .mt30 {
            margin-top: 30px;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 29px !important;
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
            <div class="card card-fluid mt-1 mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class=" col-sm-1.5 col-md-1.5  col-lg-1.5 ">
                            <div class="form-group">
                                <asp:Label ID="lblfrmDate" CssClass="control-label" runat="server" Text="From"></asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class=" col-sm-1.5  col-md-1.5  col-lg-1.5 ml-3 ">
                            <div class="form-group">
                                <asp:Label ID="lbltoDate" CssClass="control-label" runat="server" Text="To"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass=" form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3 ml-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmpName" runat="server" CssClass="control-label" Text="Employee "></asp:Label>
                                <asp:LinkButton ID="ibtnFindEmp" CssClass="srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><i class="fas fa-search"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlEmp" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" Style="width: 300px;" TabIndex="3">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary btn-sm " Style="margin-top: 20px" OnClick="lbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1  ">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="control-label" Text="Page size"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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

            <div class="card card-fluid  mt-0">
                <div class="card-body" style="min-height: 500px;">
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="vSourceWlead" runat="server">
                                <asp:GridView ID="gvCallCenter" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="616px" OnPageIndexChanging="gvCallCenter_PageIndexChanging">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <%--
                                         <asp:TemplateField HeaderText="Cluster Name"">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcluster" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="120px"> Total :</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcluster" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hmgdesc"))%>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>




                                        <%--       <asp:TemplateField FooterText="Total" HeaderText="Cluster Name">
                                            <HeaderTemplate>
                                                <table style="width: 220px">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Height="16px" Text="Cluster Name"
                                                                Width="70px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;
                                                        </td>
                                                        <td>

                                                            <asp:HyperLink ID="hlbtnCdataExel" runat="server" BackColor="#000066" BorderColor="White"
                                                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="Yellow" Style="text-align: center"
                                                                Width="80px">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hmgdesc")) %>'
                                                    Width="220"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="empid" Visible="false">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgcod" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="120px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgcod" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%--  <asp:TemplateField HeaderText="Branch Name">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBranch" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="120px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBranch" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Team Name">


                                            <HeaderTemplate>
                                                <div class="row">
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblgvheadername" runat="server">Team Name</asp:Label>

                                                    </div>


                                                    <div class="col-md-2">
                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                            CssClass="btn   btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>

                                                    </div>


                                                </div>


                                            </HeaderTemplate>



                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcluster" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="120px"> Total :</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcluster" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="P1">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="P2">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P3">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P4">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P5">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P6">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFp6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P7">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="P8">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p8")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P9">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p9")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P10">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p10")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P11">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p11")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P12">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP12" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p12")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P13">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP13" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p13")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP13" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P14">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP14" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p14")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP14" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P15">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP15" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p15")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP15" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P16">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP16" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p16")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP16" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P17">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP17" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p17")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP17" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P18">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP18" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p18")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP18" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P19">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP19" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p19")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP19" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P20">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvP20" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p20")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFP20" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "total")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="65px"></asp:Label>
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
                            </asp:View>

                            <asp:View ID="vspwiselead" runat="server">

                                <asp:GridView ID="gvsalpst" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="616px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Cluster Name">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFclustersp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="120px"> Grand Total :</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvclustersp" runat="server" Style="text-align: left" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clustname"))%>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Team Name">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvteamname" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamname"))%>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sales Person">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvsalperson" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))%>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Lead">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotallead" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlead")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtotallead" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Project Visit Done">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpvisitd" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pvisitd")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>


                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpvisitd" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Project Visit Set">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpvisitds" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pvisits")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpvisits" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Client Meeting  Done">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcmeetingd" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cmeetingd")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>


                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcmeetingd" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Client Meeting  Done">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcmeetings" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cmeetings")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>


                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcmeetings" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Follow Up">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfollowup" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "followup")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>


                                            <FooterTemplate>
                                                <asp:Label ID="lgvFfollowup" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Junk">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvjunk" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "junk")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFjunk" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="65px"></asp:Label>
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
                            </asp:View>

                            <asp:View ID="vSPWiseActivity" runat="server">
                                <asp:GridView ID="gvSPWiseActivity" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-bordered grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialno" runat="server" Style="text-align: center" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Person Name">  
                                            <FooterTemplate>
                                                <asp:Label ID="lblfootertotal" runat="server" Font-Bold="True" Font-Size="13px" 
                                                    Style="text-align: right"> Total :</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblemployeename" runat="server" Style="text-align: left"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))%>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of calls per month">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblcall" runat="server" Style="text-align: right" Width="40px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "call")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvtotalcall" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="No. of meetings with customers">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblfirstmeet" runat="server" Width="100px"   Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "firstmeeting")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvtotalfirstmeet" runat="server" Font-Bold="True" Font-Size="13px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of follow-up meeting with the prospects ">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblflowmeet" runat="server" Width="100px"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "followupmeeting")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvtotalflowmeet" runat="server" Font-Bold="True" Font-Size="13px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of customer visits to the project New ">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblvisit" runat="server" Width="100px"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "firstvisit")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalvisit" runat="server" Font-Bold="True" Font-Size="13px"
                                                     Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of project visit of prospect customer">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblflowupvisit" runat="server" Width="100px"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "followupvisit")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalflowupvisit" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotal" runat="server" Width="80px"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "total")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalfooter" runat="server" Font-Bold="True" Font-Size="13px"
                                                     Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>

                            </asp:View>

                            <asp:View ID="vRptTracking" runat="server">
                                <asp:GridView ID="gvRptTracking" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-bordered grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialno2" runat="server" Style="text-align: center" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Person Name">  
                                            <FooterTemplate>
                                                <asp:Label ID="lblftrtotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"> Total :</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblempname" runat="server" Style="text-align: left"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))%>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of Apartments sold">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblsold" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sold")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvtotalsold" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" width="145px"/>
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total turnover in amount">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblsoldamt" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soldamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvtotalsoldamt" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" width="152px"/>
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of leads generated (Self)">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblleads" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leads")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvtotalleads" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"  width="175px"/>
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of customers with over due payment">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblnoofcusoverpay" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noofcusoverpay")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvtotalnoofcusoverpay" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" width="245px"/>
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>

                                    </Columns>
                                     <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </asp:View>

                            <asp:View ID="vRptConversionDetails" runat="server">
                                <div class="row table-responsive">
                                    <asp:GridView ID="gvConversionDetails" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-bordered grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialno3" runat="server" Style="text-align: center" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                   ></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">  
                                            <FooterTemplate>
                                                <asp:Label ID="lblftrtotalCD" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"> Total :</asp:Label>
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                 <asp:Label ID="lblheaderEmpname" runat="server" Text="Sales Person Name"></asp:Label>
                                                 <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-danger btn-xs" ToolTip="Export to Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblempnameCD" runat="server" Style="text-align: left"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc"))%>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                         <%-- Query--%>
                                        <asp:TemplateField HeaderText="Query">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblQryCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qry")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalQryCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"/>
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Query">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblcurQryCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curqry")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalcurQryCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query To Hold">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblQryToholdCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qrytohold")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalQryTohldCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"/>
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query To Lost">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblQryTolostCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qrytolost")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalQryTolostCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query To Lead">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblQryToleadCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qrytolead")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalQryToleadCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"/>
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query To Lead %">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblQryToleadperCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qrytoleadper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalQryToleadperCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>


                                         <%-- Lead--%>
                                        <asp:TemplateField HeaderText="Lead">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeadCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lead")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalLeadCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Lead">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblcurleadCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curlead")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalcurLeadCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lead To Hold">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeadToholdCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leadtohold")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalLeadTohldCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lead To Lost">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeadTolostCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leadtolost")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalLeadTolostCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lead To Qualified Lead">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeadToqleadCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leadtoqlead")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalLeadToqleadCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lead To Qualified Lead %">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeadToqleadperCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leadtoqleadper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalLeadToqleadperCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>                                        


                                         <%-- Qualified lead--%>
                                        <asp:TemplateField HeaderText="Qualified Lead">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblQLeadCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qlead")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalQLeadCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Qualified Lead">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblcurQleadCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curqlead")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalcurQLeadCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"/>
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Qualified Lead To Hold">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblqLeadToholdCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qleadtohold")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalqLeadTohldCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qualified Lead To Lost">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblqLeadTolostCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qleadtolost")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalqLeadTolostCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"/>
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qualified Lead To Nego">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblqLeadToqleadCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qleadtonego")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalqLeadToqleadCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qualified Lead To  Nego %">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblqLeadToNegoperCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qleadtonegoper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalqLeadToNegoperCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"/>
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>

                                        
                                         <%-- Nego--%>
                                        <asp:TemplateField HeaderText="Nego">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegoCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nego")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalnegoCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Nego">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblcurnegoCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curnego")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalcurnegoCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Nego To Hold">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblqNegoToholdCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "negotohold")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalNegoTohldCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nego To Lost">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegoTolostCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "negotolost")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalnegoTolostCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Win">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegoToWinCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "negotowin")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalnegoToWinCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nego To Win %">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegoTowinperCD" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "negotowinper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalnegoToWinperCD" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                                </div>
                                
                            </asp:View>

                            <asp:View ID="vRptConversion" runat="server">
                                <div class="row table-responsive">
                                    <asp:GridView ID="gvConversion" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-bordered grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialno4" runat="server" Style="text-align: center" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                   ></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">  
                                            <FooterTemplate>
                                                <asp:Label ID="lblftrtotalC" runat="server" Font-Bold="True" Font-Size="12px" Visible="false"
                                                    Style="text-align: right"> Total :</asp:Label>
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                 <asp:Label ID="lblheaderEmpnameC" runat="server" Text="Sales Person Name"></asp:Label>
                                                 <asp:HyperLink ID="hlbtntbCdataExelC" runat="server" CssClass="btn btn-danger btn-xs" ToolTip="Export to Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblempnameC" runat="server" Style="text-align: left"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc"))%>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query To Lead %">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblQryToleadperC" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qrytoleadper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalQryToleadperC" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Lead To Qualified Lead %">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeadToqleadperC" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leadtoqleadper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalLeadToqleadperC" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qualified Lead To  Nego %">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblqLeadToNegoperC" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qleadtonegoper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalqLeadToNegoperC" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"/>
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nego To Win %">                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegoTowinperC" runat="server" Style="text-align: right"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "negotowinper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalnegoToWinperC" runat="server" Font-Bold="True" Font-Size="13px"
                                                   Style="text-align: right;"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right"/>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                             </div>
                        </asp:View>


                        </asp:MultiView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

