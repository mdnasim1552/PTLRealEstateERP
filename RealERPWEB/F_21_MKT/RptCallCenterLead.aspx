
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptCallCenterLead.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptCallCenterLead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {


            var gvCallCenter = $('#<%=this.gvCallCenter.ClientID %>');

            gvCallCenter.Scrollable();



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
           <div class="card card-fluid container-data">
                <div class="card-body" style="min-height: 600px;">
                    <div class="row">  
                                    <div class=" col-md-1 ">
                                          <div class="form-group">
                                        <asp:Label ID="lblfrmDate" CssClass="control-label" runat="server" Text="From Date"></asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>
                                          </div>
                                        </div>
                                              <div class=" col-md-1 ">
                                          <div class="form-group">
                                        <asp:Label ID="lbltoDate" CssClass="control-label" runat="server" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                                    </div>

                                    <div class="clearfix"></div>
                                </div>
                        <div class="col-md-1">
                              <div class="form-group">
                                        <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary btn-sm " style="margin-top:20px" OnClick="lbtnShow_Click" >Show</asp:LinkButton>
                                  </div>
                        </div>
                                    <div class="col-md-3  ">
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
                          </asp:MultiView>

                    </div>
               </div>
                  
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

