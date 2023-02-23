<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccountsReport.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptAccountsReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);




        });
        function pageLoaded() {

            $('#tblrpcashflow').gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 2


            });
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">


                                    <div class="col-md-4  pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="smLbl_to"
                                            Text="From:"></asp:Label>

                                        <asp:TextBox ID="txtfrmDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to"
                                            Text="To:"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        <asp:LinkButton ID="lbtnExpExcel" runat="server" CssClass="btn btn-primary primaryBtn" ToolTip="Export Excel" OnClick="lbtnExpExcel_Click">Export</asp:LinkButton>

                                    </div>

                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="viewCashFlow" runat="server">
                            <div class="row table-responsive">

                                <asp:Repeater ID="rpcashflow" runat="server" OnItemDataBound="rpcashflow_ItemDataBound">
                                    <HeaderTemplate>
                                        <table id="tblrpcashflow" class="table-striped table-hover table-bordered grvContentarea">
                                            <tr>
                                                <th>SL</th>
                                                <th>Description
                                                </th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt01" runat="server" Width="70px"></asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt02" runat="server" Width="70px"></asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt03" runat="server" Width="70px"></asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt04" runat="server" Width="70px"></asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt05" runat="server" Width="70px"></asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt06" runat="server" Width="70px"></asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt07" runat="server" Width="70px"></asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt08" runat="server" Width="70px"></asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt09" runat="server" Width="70px"></asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt10" runat="server" Width="70px"></asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt11" runat="server" Width="70px"></asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt12" runat="server" Width="70px"></asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt13" runat="server" Width="70px"> </asp:Label></th>

                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt14" runat="server" Width="70px"> </asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt15" runat="server" Width="70px"> </asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt16" runat="server" Width="70px"> </asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt17" runat="server" Width="70px"> </asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt18" runat="server" Width="70px"> </asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt19" runat="server" Width="70px"> </asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt20" runat="server" Width="70px"> </asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt21" runat="server" Width="70px"> </asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt22" runat="server" Width="70px"> </asp:Label></th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt23" runat="server" Width="70px"> </asp:Label></th>
                                                 <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt24" runat="server" Width="70px"> </asp:Label></th>
                                                 <th style="width: 70px;">
                                                    <asp:Label ID="lblrphamt25" runat="server" Width="70px"> </asp:Label></th>

                                                <th style="width: 70px;">Total</th>


                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right" Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </td>
                                            <td>



                                                <asp:Label ID="lgvProjectName" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "flowdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "flowdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                     
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="160px"></asp:Label>
                                            </td>


                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt01" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>

                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt02" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt03" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt04" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt05" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt06" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt07" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt08" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt09" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt10" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt11" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt12" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt13" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt13")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt14" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt14")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>

                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt15" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt15")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>

                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt16" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt16")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>

                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt17" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt17")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt18" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt18")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt19" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt19")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt20" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt20")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt21" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt21")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt22" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt22")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt23" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt23")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt24" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt24")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpamt25" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt25")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>

                                            <td style="text-align: right">
                                                <asp:Label ID="lblrptoamt" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>' Width="70px" Font-Bold="true"></asp:Label>
                                            </td>


                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr>
                                            <th></th>
                                            <th>Total</th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt01" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt02" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt03" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt04" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt05" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt06" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt07" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt08" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt09" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt10" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt11" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt12" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt13" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="Label1" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt14" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt15" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt16" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt17" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt18" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt19" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt20" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt21" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFamt22" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFtoamt" runat="server" Width="80px"></asp:Label>
                                            </th>
                                        </tr>
                                        </table>
                                    </FooterTemplate>


                                </asp:Repeater>


                            </div>


                        </asp:View>

                        <asp:View ID="Viewcrlim02" runat="server">

                            <div class="row">
                            </div>
                        </asp:View>


                    </asp:MultiView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
