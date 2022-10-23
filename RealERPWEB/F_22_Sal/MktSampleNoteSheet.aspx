<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktSampleNoteSheet.aspx.cs" Inherits="RealERPWEB.F_22_Sal.MktSampleNoteSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/WebForms/Bootstrapautocomplete.js"></script>--%>

    <style>
        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            margin: 0px 0 0 0px;
            font-size: 11px;
            font-weight: normal;
            border: solid 1px #006699;
            background-color: White;
        }

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
    </style>

    <script language="javascript" type="text/javascript">

        var src = {

            "jQuery": 1,

            "Script": 2,

            "HTML5": 3,

            "CSS3": 4,

            "Angular": 5,

            "React": 6,

            "VueJS": 7

        };



        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            try {


                console.log(src);

                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });


                var gvcoff = $('#<%=this.gvcoffsch.ClientID %>');


                gvcoff.Scrollable();

                $('.chzn-select').chosen({ search_contains: true });




                //$('#txtProspective').autocomplete({


                //    source: src

                //});

                //$('#txtProspective').autocomplete({
                //    treshold: 1

                //});

                //$('#txtProspective').autocomplete({

                //    maximumItems: 3

                //});



                // $('#myAutocomplete').autocomplete({


                //    source: src

                //});

                //$('#myAutocomplete').autocomplete({
                //    treshold: 1

                //});

                //$('#myAutocomplete').autocomplete({

                //    maximumItems: 3

                //});




            }

            catch (e) {
                alert(e);


            }


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

        .form-control-sm {
            padding: 0.25rem 0rem !important;
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

            <div class="card card-fluid mb-1 mt-4">
                <div class="card-body">
                    <div class="row ">
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

                        <div class="col-md-2">
                            <div class="form-group">
                                <label id="Label1" runat="server">Prospective</label>
                                <asp:LinkButton ID="lbtnProspective" runat="server" OnClick="lbtnProspective_Click"> <i class="fa fa-search" aria-hidden="true"></i>
                                </asp:LinkButton>
                                <asp:DropDownList ID="ddlprospective" runat="server" CssClass="form-control chzn-select" TabIndex="12">
                                </asp:DropDownList>


                                <%--   <asp:TextBox ID="txtProspective" runat="server" CssClass="form-control" Width="130"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="txtProspective_AutoCompleteExtender"
                                                                            runat="server" CompletionListCssClass="AutoExtender"
                                                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                                            CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="15"
                                                                            DelimiterCharacters="" Enabled="True" FirstRowSelected="True"
                                                                            MinimumPrefixLength="0" ServiceMethod="GetprospectiveDetails"
                                                                            ServicePath="~/AutoCompleted.asmx" TargetControlID="txtProspective">
                                                                        </cc1:AutoCompleteExtender>--%>
                            </div>

                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label id="lblprevious" runat="server">Previous</label>
                                <asp:LinkButton ID="lnkbtnPrevious" runat="server" OnClick="lnkbtnPrevious_Click"> <i class="fa fa-search" aria-hidden="true"></i>
                                </asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevious" runat="server" CssClass="form-control chzn-select" TabIndex="12">
                                </asp:DropDownList>
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

                        <div class="col-md-3 ">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnBack" runat="server" OnClick="lbtnBack_Click" Visible="false" CssClass="btn btn-danger btn-sm  pull-right " Style="margin-left: 159px;">Back</asp:LinkButton>

                                <asp:Label ID="lblCode" runat="server" Visible="False" Width="63px"></asp:Label>
                            </div>

                        </div>




                    </div>

                </div>
            </div>




            <asp:MultiView ID="MultiView1" runat="server">

                <asp:View ID="ViewSchedule" runat="server">
                    <div class="card card-fluid mb-0">
                        <div class="card-body">
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="card card-fluid">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin lblheadertitle">
                                                        <label id="lblheadercoffer" runat="server">Customer Offer</label>
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="row">

                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="lblcoffarea" runat="server">Area(in sft)</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblvalcoffarea" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">



                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lblcoffurate" runat="server">Rate(BDT/sft)</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcoffrate" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltxtcoffunitprice" runat="server">Unit Value(BDT)</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblcoffunitprice" runat="server" clss="form-control form-control-sm">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lblcoffparking" runat="server">Parking</label>
                                                    </div>

                                                </div>


                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcofffparking" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltxtcoffutility" runat="server">Utility</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcoffutility" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltxtcoffothers" runat="server">Others</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcoffothers" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin font-weight-bold ">
                                                        <label id="lbltxtcoffTotal" runat="server">Total</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright font-weight-bold">

                                                        <label id="lblcoffTotal" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltxtcoffbookinmpercnt" runat="server">Booking Money %</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcoffbookinmpercnt" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltxtvalcoffbookingam" runat="server">Booking Money</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblvalcoffbookingam" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltxtcoffnooffemi" runat="server">No. of EMI</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcoffnooffemi" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="display: none;">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltxtcoffemi" runat="server">EMI</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblvalcoffemi" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="display: none;">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin font-weight-bold">
                                                        <label id="lbltxtcofffvpersft" runat="server">FV per SFT</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright font-weight-bold">
                                                        <label id="lblvalcofffvpersft" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="display: none;">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin font-weight-bold">
                                                        <label id="lbltxtcoffpvpersft" runat="server">PV per SFT</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright font-weight-bold">
                                                        <label id="lblvalcoffpvpersft" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">

                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="lblcoffBookingdat" runat="server">B. Date</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin">

                                                        <asp:TextBox ID="txtcoffBookingdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtcoffBookingdate_CalendarExtender1" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtcoffBookingdate"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">


                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="lblcoffinsdate" runat="server">Ins. Date</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright ">

                                                        <asp:TextBox ID="txtcoffinsdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtcoffinsdate_CalendarExtender" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtcoffinsdate"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="lblcoffduration" runat="server">Duration</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
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

                                            <div class="row" style="display: none">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="lblinrate" runat="server">Interest Rate</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <asp:TextBox ID="txtinterestrate" runat="server" CssClass="form-control form-control-sm textalignright" Text="9%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row" style="margin-top: 10px;">


                                                <div class="col-md-4 offset-4">
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="lbtnCalCulation" runat="server" CssClass=" form-control form-control-sm  btn  btn-warning" OnClick="lbtnCalCulation_Click">Calculation</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass=" form-control form-control-sm  btn btn-success btn-sm" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">

                                    <div class="card card-fluid">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group lblmargin lblheadertitle">
                                                        <label id="Label5" runat="server">Schedule Information</label>
                                                    </div>

                                                </div>



                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:CheckBox ID="chkSegment" runat="server" AutoPostBack="True"
                                                            CssClass="chkBoxControl"
                                                            OnCheckedChanged="chkSegment_CheckedChanged" Text="Slab" />


                                                    </div>




                                                    <asp:GridView ID="gvcoffsch" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                        AutoGenerateColumns="False" ShowFooter="True"
                                                        Style="margin-right: 0px">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvslnodumpay" runat="server"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:LinkButton ID="lbtnAdddsch" OnClick="lbtnAdddsch_Click" ToolTip="Add Installment" runat="server" ><span  class=" fa fa-plus" aria-hidden="true"></span> </asp:LinkButton>
                                                                   


                                                                </ItemTemplate>
                                                                <ItemStyle Width="30px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                             
                                                            
                                                             <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:LinkButton ID="lbtnDeldsch" OnClick="lbtnDeldsch_Click" ToolTip="Delete Installment" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="fa fa-trash"></span> </asp:LinkButton>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="30px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Month">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblschmonth" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="none"
                                                                        Height="16px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ymon"))%>'
                                                                        Width="60px"></asp:Label>

                                                                   

                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                   
                                                                    
                                                                    <asp:LinkButton ID="lnkgvFcoffTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Text="Total" OnClick="lnkgvFcoffTotal_Click"></asp:LinkButton>
                                                                </FooterTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                                            </asp:TemplateField>



                                                              <asp:TemplateField HeaderText="Schedule Date">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvScheduledate" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="none"
                                                                      
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ymon"))%>'
                                                                        Width="80px"></asp:TextBox>

                                                                    <cc1:CalendarExtender ID="txtgvScheduledate_CalendarExtender1" runat="server"
                                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvScheduledate"></cc1:CalendarExtender>

                                                                </ItemTemplate>

                                                              

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="PV">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvdumschamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pv")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="80px"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFcoffpvschamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />

                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="FV" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcofffvschamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fv")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFcofffvscham" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />

                                                            </asp:TemplateField>








                                                        </Columns>
                                                        <FooterStyle CssClass="" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="" />
                                                        <HeaderStyle CssClass="" />
                                                    </asp:GridView>

                                                </div>




                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <asp:Panel ID="pnlSlab" runat="server" Visible="False">
                                        <div class="card card-fluid">
                                            <div class="card-body">
                                                <div class="row">

                                                    <div class="col-md-5">
                                                        <div class="form-group lblmargin">
                                                            <label id="lblfrmslab" runat="server">From</label>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-7">
                                                        <div class="form-group lblmargin">

                                                            <asp:TextBox ID="txtfrmslab" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="row">

                                                    <div class="col-md-5">
                                                        <div class="form-group lblmargin">
                                                            <label id="lbltoslab" runat="server">To</label>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-7">
                                                        <div class="form-group lblmargin">

                                                            <asp:TextBox ID="txttoslab" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="row">

                                                    <div class="col-md-5">
                                                        <div class="form-group lblmargin">
                                                            <label id="Label2" runat="server">Installment</label>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-7">
                                                        <div class="form-group lblmargin">

                                                            <asp:TextBox ID="txtperslabamt" runat="server" CssClass="form-control  form-control-sm"> </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>



                                                <div clsss="row" style="margin: 10px 0px 0px 10px;">

                                                    <div class="col-md-7 offset-5">
                                                        <div class="form-group">
                                                            <asp:LinkButton ID="lbtnSlab" runat="server" CssClass=" form-control form-control-sm  btn btn-warning" OnClick="lbtnSlab_Click">Put Data</asp:LinkButton>
                                                        </div>


                                                    </div>

                                                </div>


                                            </div>

                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                    </div>








                </asp:View>
            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>




