<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktGrandNoteSheet.aspx.cs" Inherits="RealERPWEB.F_22_Sal.MktGrandNoteSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });

        }
    </script>
    <style>
        .grvHeader th {
            font-weight: normal;
            text-align: center;
            text-transform: capitalize;
        }
        /*.cald {
             z-index: 1;
        }*/

        .lblmargin {
            margin: 0px !important;
        }

        .lblheadertitle {
            background-color: #346CB0;
            font-size: 14px;
            font-weight: bold;
            color: white;
            padding-left: 5px !important;
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

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label id="lblprojectname" runat="server">Project Name</label>
                                <asp:LinkButton ID="lnkbtnFindProject" runat="server" OnClick="lnkbtnFindProject_Click"> <i class="fa fa-search" aria-hidden="true"></i>
                                </asp:LinkButton>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select" TabIndex="12">
                                </asp:DropDownList>
                                <asp:Label ID="lblProjectmDesc" runat="server" Visible="False" CssClass="form-control "></asp:Label>

                            </div>


                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label id="lblunitname" runat="server">Unit Name</label>
                                <asp:LinkButton ID="lbtnsrchunit" runat="server" OnClick="lbtnsrchunit_Click"> <i class="fa fa-search" aria-hidden="true"></i>
                                </asp:LinkButton>
                                <asp:TextBox ID="txtsrchunit" runat="server" CssClass="form-control"></asp:TextBox>


                            </div>


                        </div>
                        <div class="col-md-1">
                            <div class="form-group">

                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass=" btn btn-primary btn-sm  margin-top30px" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                            </div>

                        </div>
                    </div>

                </div>
            </div>


            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-6">
                            <asp:GridView ID="gvSpayment" runat="server" AutoGenerateColumns="False" Width="831px" CssClass=" table-striped table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" HeaderStyle-Width="80px" CancelText="&lt;span class='glyphicon glyphicon-remove pull-left'&gt;&lt;/span&gt;" DeleteText="&lt;span class='glyphicon glyphicon-remove'&gt;&lt;/span&gt;" EditText="&lt;span class='glyphicon glyphicon-pencil'&gt;&lt;/span&gt;" UpdateText="&lt;span class='glyphicon glyphicon-ok'&gt;&lt;/span&gt;" />

                                    <asp:TemplateField HeaderText="Sl.No.">



                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Size">

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>


                                            <asp:LinkButton ID="lbtnusize" runat="server" CommandArgument="lbtnusize"
                                                OnClick="lbtnusize_Click" Style="text-align: right;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>


                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Parking">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvminbmoneyaa" runat="server" Style="text-align: right" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Utility">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvminbmvoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Others">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmincbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cooperative")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvminsbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Min Booking Money" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvminbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minbam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Mgt Booking" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMgtBook" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgtbook1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle CssClass="grvFooterNew" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="" />
                                <HeaderStyle CssClass="grvHeaderNew" />
                            </asp:GridView>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnBack" runat="server" OnClick="lbtnBack_Click" Visible="false" CssClass="btn btn-danger btn-sm  pull-right">Back</asp:LinkButton>
                                    
                                 <asp:Label ID="lblCode" runat="server" Visible="False" Width="63px"></asp:Label>
                            </div>

                        </div>




                    </div>

                </div>
            </div>




            <asp:MultiView ID="MultiView1" runat="server">

                <asp:View ID="ViewSchedule" runat="server">
                    <div class="card card-fluid mb-1">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-2">

                                    <div class="card card-fluid">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin lblheadertitle">
                                                        <label id="lblbasecase" runat="server">A. Base Case</label>
                                                    </div>

                                                </div>

                                            </div>


                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="lblarea" runat="server">Area(in sft)</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblvalarea" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lblrate" runat="server">Rate(BDT/sft)</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblvalrate" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lblunitprice" runat="server">Unit Value(BDT)</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblvalunitprice" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltxtparking" runat="server">Parking</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblvalparking" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lblutility" runat="server">Utility</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblvalutility" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lblother" runat="server">Others</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblvalother" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin font-weight-bold ">
                                                        <label id="lblTotal" runat="server">Total</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright font-weight-bold">

                                                        <label id="lblvalTotal" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lblbookingpercnt" runat="server">Booking Money %</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblvalbookingpercnt" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lblbookingmoney" runat="server">Booking Money</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblvalbookingmoney" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lblnoofemi" runat="server">No. of EMI</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblvalnoofemi" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lblemi" runat="server">EMI</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblvalemi" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin font-weight-bold">
                                                        <label id="lblfvpsft" runat="server">FV per SFT</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright font-weight-bold">

                                                        <label id="lblvalfvpsft" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin font-weight-bold">
                                                        <label id="lblpvpersft" runat="server">PV per SFT</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright font-weight-bold">

                                                        <label id="lblvalpvpersft" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin">
                                                        <label id="lblinsdate" runat="server">B. Date</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">

                                                        <asp:TextBox ID="txtBookingdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtBookingdate_CalendarExtender" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtBookingdate"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin">
                                                        <label id="Label2" runat="server">Ins. Date</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin textalignright ">

                                                        <asp:TextBox ID="txtfirstinsdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtfirstinsdate_CalendarExtender" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtfirstinsdate"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin">
                                                        <label id="Label3" runat="server">Duration</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">

                                                        <asp:DropDownList ID="ddlduration" runat="server"  
                                                            CssClass="form-control form-control-sm chzn-select">
                                                            <asp:ListItem Value="1">1 Month</asp:ListItem>
                                                            <asp:ListItem Value="2">2 Month</asp:ListItem>
                                                            <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                                            <asp:ListItem Value="4">4 Month</asp:ListItem>
                                                            <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                                            <asp:ListItem Value="6">6 Month</asp:ListItem>
                                                            <asp:ListItem Value="7">7 Month</asp:ListItem>
                                                            <asp:ListItem Value="8">8 Month</asp:ListItem>
                                                            <asp:ListItem Value="9">9 Month</asp:ListItem>
                                                            <asp:ListItem Value="10">10 Month</asp:ListItem>
                                                            <asp:ListItem Value="11">11 Month</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-2">
                                    <div class="card card-fluid">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin lblheadertitle">
                                                        <label id="lblheadercoffer" runat="server">B. Customer Offer</label>
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblvalcoffarea" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcoffrate" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblcoffunitprice" runat="server" clss="form-control form-control-sm">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcofffparking" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcoffutility" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcoffothers" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright font-weight-bold">

                                                        <label id="lblcoffTotal" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcoffbookinmpercnt" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblcoffbookingam" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcoffnooffemi" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblcoffemi" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright font-weight-bold">
                                                        <label id="lblcofffvpersft" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright font-weight-bold">
                                                        <label id="lblcoffpvpersft" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin">

                                                        <asp:TextBox ID="txtcoffBookingdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtcoffBookingdate_CalendarExtender1" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtcoffBookingdate"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright ">

                                                        <asp:TextBox ID="txtcoffinsdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtcoffinsdate_CalendarExtender" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtcoffinsdate"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin">

                                                        <asp:DropDownList ID="ddlcoffduration" runat="server" 
                                                            CssClass="form-control form-control-sm chzn-select">
                                                            <asp:ListItem Value="1">1 Month</asp:ListItem>
                                                            <asp:ListItem Value="2">2 Month</asp:ListItem>
                                                            <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                                            <asp:ListItem Value="4">4 Month</asp:ListItem>
                                                            <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                                            <asp:ListItem Value="6">6 Month</asp:ListItem>
                                                            <asp:ListItem Value="7">7 Month</asp:ListItem>
                                                            <asp:ListItem Value="8">8 Month</asp:ListItem>
                                                            <asp:ListItem Value="9">9 Month</asp:ListItem>
                                                            <asp:ListItem Value="10">10 Month</asp:ListItem>
                                                            <asp:ListItem Value="11">11 Month</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="card card-fluid">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin lblheadertitle">
                                                        <label id="lblrevoffer" runat="server">C. RCU Offer</label>
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblrevparea" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtrevprate" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblrevpunitprice" runat="server" clss="form-control form-control-sm">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtrevpparking" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtrevputility" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtrevpothers" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright font-weight-bold">

                                                        <label id="lblrevpTotal" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtrevpbbookinmpercnt" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblrevpbookingam" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtrevpnooffemi" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblrevpemi" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright font-weight-bold">
                                                        <label id="lblrevpfvpersft" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright font-weight-bold">
                                                        <label id="lblrevppvpersft" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin">

                                                        <asp:TextBox ID="txtrevpBookingdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtrevpBookingdate_CalendarExtender" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtrevpBookingdate"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin textalignright ">

                                                        <asp:TextBox ID="txtrevpinsdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtrevpinsdate_CalendarExtender" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtrevpinsdate"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin">

                                                        <asp:DropDownList ID="ddlrevpduration" runat="server" 
                                                            CssClass="form-control form-control-sm chzn-select">
                                                            <asp:ListItem Value="1">1 Month</asp:ListItem>
                                                            <asp:ListItem Value="2">2 Month</asp:ListItem>
                                                            <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                                            <asp:ListItem Value="4">4 Month</asp:ListItem>
                                                            <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                                            <asp:ListItem Value="6">6 Month</asp:ListItem>
                                                            <asp:ListItem Value="7">7 Month</asp:ListItem>
                                                            <asp:ListItem Value="8">8 Month</asp:ListItem>
                                                            <asp:ListItem Value="9">9 Month</asp:ListItem>
                                                            <asp:ListItem Value="10">10 Month</asp:ListItem>
                                                            <asp:ListItem Value="11">11 Month</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">

                                    <div class="card card-fluid">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin lblheadertitle">
                                                        <label id="Label4" runat="server">A. Base Case</label>
                                                    </div>

                                                </div>
                                                <asp:GridView ID="gvbcasesch" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="398px"
                                Style="margin-right: 0px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvslnodumpay" runat="server" Font-Bold="True" ForeColor="Black"
                                                Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                   


                                    <asp:TemplateField HeaderText="Schedule Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvScheduledate" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="none"
                                                Height="16px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:TextBox>

                                            <cc1:CalendarExtender ID="txtgvScheduledate_CalendarExtender1" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvScheduledate"></cc1:CalendarExtender>

                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotaldumsch" CssClass="btn btn-primary  btn-xs" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Text="Total" OnClick="lbtnTotaldumsch_Click"></asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvdumschamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFdumschamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="FV">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvfvschamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFfvscham" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lbtnAdddsch" OnClick="lbtnAdddsch_Click" ToolTip="Add Installment" runat="server" Visible="false"><span  class="glyphicon glyphicon-plus"></span> </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnDeldsch" OnClick="lbtnDeldsch_Click" ToolTip="Delete Installment" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>


                                        </ItemTemplate>
                                        <ItemStyle Width="60px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Top" />
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
                                    </div>

                                </div>

                                <div class="col-md-2">

                                    <div class="card card-fluid">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin lblheadertitle">
                                                            <label id="Label5" runat="server">B. Customer Offer</label>
                                                    </div>

                                                </div>
                                                 <asp:GridView ID="gvcoffsch" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="398px"
                                Style="margin-right: 0px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvslnodumpay" runat="server" Font-Bold="True" ForeColor="Black"
                                                Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                   


                                   
                                    <asp:TemplateField HeaderText="Schedule Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvScheduledate" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="none"
                                                Height="16px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:TextBox>

                                            <cc1:CalendarExtender ID="txtgvScheduledate_CalendarExtender1" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvScheduledate"></cc1:CalendarExtender>

                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotaldumsch" CssClass="btn btn-primary  btn-xs" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Text="Total" OnClick="lbtnTotaldumsch_Click"></asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Schedule Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvdumschamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFdumschamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>


                                         <asp:TemplateField HeaderText="FV">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvcofffvschamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcofffvscham" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lbtnAdddsch" OnClick="lbtnAdddsch_Click" ToolTip="Add Installment" runat="server" Visible="false"><span  class="glyphicon glyphicon-plus"></span> </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnDeldsch" OnClick="lbtnDeldsch_Click" ToolTip="Delete Installment" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>


                                        </ItemTemplate>
                                        <ItemStyle Width="60px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Top" />
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
                                    </div>

                                </div>

                                 <div class="col-md-2">

                                    <div class="card card-fluid">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin lblheadertitle">
                                                            <label id="Label6" runat="server">C. RCU Offer</label>
                                                    </div>

                                                </div>


                                                 <asp:GridView ID="gvrevpsch" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="398px"
                                Style="margin-right: 0px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvslnodumpay" runat="server" Font-Bold="True" ForeColor="Black"
                                                Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                   


                                   
                                    <asp:TemplateField HeaderText="Schedule Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvScheduledate" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="none"
                                                Height="16px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:TextBox>

                                            <cc1:CalendarExtender ID="txtgvScheduledate_CalendarExtender1" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvScheduledate"></cc1:CalendarExtender>

                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotaldumsch" CssClass="btn btn-primary  btn-xs" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Text="Total" OnClick="lbtnTotaldumsch_Click"></asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Schedule Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvdumschamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFdumschamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    
                                         <asp:TemplateField HeaderText="FV">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvrevpfvschamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFrevpfvscham" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lbtnAdddsch" OnClick="lbtnAdddsch_Click" ToolTip="Add Installment" runat="server" Visible="false"><span  class="glyphicon glyphicon-plus"></span> </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnDeldsch" OnClick="lbtnDeldsch_Click" ToolTip="Delete Installment" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>


                                        </ItemTemplate>
                                        <ItemStyle Width="60px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Top" />
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
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>







               
                </asp:View>
            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>




