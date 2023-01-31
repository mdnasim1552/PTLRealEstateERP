<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktSampleNoteSheet.aspx.cs" Inherits="RealERPWEB.F_22_Sal.MktSampleNoteSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">





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

    <script type="text/javascript">



        $(document).ready(function () {


            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            //$("#txtprospective").autocomplete({
            //    source: prospective
            //});


        });
        function pageLoaded() {

            try {






                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });


                var gvbcase = $('#<%=this.gvbcasesch.ClientID%>');
                gvbcase.Scrollable();
                var gvcoff = $('#<%=this.gvcoffsch.ClientID %>');
                gvcoff.Scrollable();



                $('.chzn-select').chosen({ search_contains: true });


                var obj = new RealERPScript();
                var comcod =<%=this.GetCompCode()%>;
                var empid =<%=this.GetEmpid()%>;
                var type = 'SalesTeam';


                var lstprospec = obj.GetProspective(comcod, empid, type);
                console.log(lstprospec);
                var prospec = JSON.parse(lstprospec);
                var prospective = [];

                $.each(prospec, function (index, prospec) {

                    prospective.push(prospec.prosdesc);

                });




                $("#txtprospective").autocomplete({
                    source: prospective
                });



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

        .grvContentarea {
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
                                <asp:TextBox ID="txtprospective" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
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
                        <div class="col-md-2">
                            <label class="control-label" for="ddlUserName" id="Label3" runat="server">Type</label>
                            <asp:DropDownList ID="ddlPrintType" runat="server" CssClass="form-control chzn-select" TabIndex="12">
                                <asp:ListItem Value="samnotesheet" Enabled="true">Sample Note Sheet</asp:ListItem>
                                <asp:ListItem Value="grandnotesheet">Grand Note Sheet(Summary)</asp:ListItem>
                                <asp:ListItem Value="grandnotesheetdet">Grand Note Sheet(Details)</asp:ListItem>
                            </asp:DropDownList>
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
                            <asp:GridView ID="gvSpayment" runat="server" AutoGenerateColumns="False" Width="400px" CssClass=" table-striped table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" HeaderStyle-Width="80px" CancelText="&lt;span class='glyphicon glyphicon-remove pull-left'&gt;&lt;/span&gt;" DeleteText="&lt;span class='glyphicon glyphicon-remove'&gt;&lt;/span&gt;" EditText="&lt;span class='glyphicon glyphicon-pencil'&gt;&lt;/span&gt;" UpdateText="&lt;span class='glyphicon glyphicon-ok'&gt;&lt;/span&gt;" />

                                    <asp:TemplateField HeaderText="Sl">



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
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit" runat="server" Width="60px"
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

                                    <asp:TemplateField HeaderText="Rate" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>


                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" Visible="false">

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Parking" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvminbmoneyaa" runat="server" Style="text-align: right" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Utility" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvminbmvoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Others" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmincbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cooperative")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Amount" Visible="false">
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
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
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

                                <div class="col-md-3">

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
                                                        <label id="lbltxtdownpayper" runat="server">Down Payment  %</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblvaldownpayper" runat="server" clss="form-control form-control-sm ">2500</label>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltxtdownpayam" runat="server">Down Payment</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblvaldownpayam" runat="server" clss="form-control form-control-sm "></label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">

                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="lbltxtdownpaydate" runat="server">Down Payment Date</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin">

                                                        <asp:TextBox ID="txtdownpaydate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender_txtdownpaydate" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtdownpaydate"></cc1:CalendarExtender>
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
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="lblfinstallmentper" runat="server">Final Installment %</label>
                                                    </div>

                                                </div>


                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin  font-weight-bold">

                                                        <label id="lblfvalinstallmentper" runat="server" clss="form-control form-control-sm "></label>
                                                    </div>
                                                </div>


                                            </div>



                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="lbltxthandovdate" runat="server">Handover  Date</label>
                                                    </div>

                                                </div>


                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin  font-weight-bold">

                                                        <label id="lblvalhandovdate" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>


                                            </div>











                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="Label6" runat="server">Interest Rate</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <asp:TextBox ID="txtInterestRateBase" runat="server" CssClass="form-control form-control-sm textalignright" Text="9%"></asp:TextBox>
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
                                                        <label id="Label7" runat="server">Schedule Information(Base Case)</label>
                                                    </div>

                                                </div>
                                                <asp:GridView ID="gvbcasesch" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    AutoGenerateColumns="False" ShowFooter="True"
                                                    Style="margin-right: 0px">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvslnodumpay" runat="server"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbcaseschdesc" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="none"
                                                                    Height="16px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                                                    Width="120px"></asp:Label>



                                                            </ItemTemplate>
                                                            <FooterTemplate>


                                                                <asp:LinkButton ID="lnkgvbaseFcoffTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Black" Style="text-align: right" Text="Total" OnClick="lnkgvbaseFcoffTotal_Click"></asp:LinkButton>
                                                            </FooterTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Schedule Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvScheduledate" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="none"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy")%>'
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
                                                                <asp:Label ID="lgvFpvschamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FV">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvfvschamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fv")).ToString("#,##0;(#,##0); ") %>'
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

                                <div class="col-md-3">
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
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblcoffTotal" runat="server" class="font-weight-bold">2500</label>
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
                                                        <asp:TextBox ID="txtcoffbookingam" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">

                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="lblcoffBookingdat" runat="server">Booking Date</label>
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
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltxtcoffdownpayper" runat="server">Down Payment  %</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <asp:TextBox ID="txtcoffdownpayper" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltxtcoffdownpayam" runat="server">Down Payment</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblvalcoffdownpayam" runat="server" clss="form-control form-control-sm "></label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">

                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="lbltxtcoffdownpaydate" runat="server">Down Payment Date</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin">

                                                        <asp:TextBox ID="txtcoffdownpaydate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender_txtcoffdownpaydate" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtcoffdownpaydate"></cc1:CalendarExtender>
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
                                                        <asp:TextBox ID="txtcoffnooffemi" runat="server" CssClass="form-control form-control-sm textalignright" AutoPostBack="true" OnTextChanged="txtcoffnooffemi_TextChanged"></asp:TextBox>
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
                                            <div class="row" >
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

                                            <div class="row" >
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
                                                        <label id="lblcoffinsdate" runat="server">Installment Date</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright ">

                                                        <asp:TextBox ID="txtcoffinsdate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnTextChanged="txtcoffinsdate_TextChanged"></asp:TextBox>
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

                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lblcofffininspercnt" runat="server">Final Installment %</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin">

                                                        <asp:TextBox ID="txtcofffininsper" runat="server" CssClass="form-control form-control-sm textalignright"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltxtcofffininsam" runat="server">Final Installment</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblvalcofffininsam" runat="server" clss="form-control form-control-sm ">2500</label>
                                                    </div>
                                                </div>

                                            </div>


                                            <div class="row">


                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="lblcofffininsdate" runat="server">Final Installment Date</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright ">

                                                        <asp:TextBox ID="txtcofffininsdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender_txtcofffininsdate" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="txtcofffininsdate"></cc1:CalendarExtender>
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
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="Label4" runat="server">Difference in FV</label>
                                                    </div>


                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblDiffFV" runat="server" clss="form-control form-control-sm "></label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin">
                                                        <label id="Label8" runat="server">Difference in PV</label>
                                                    </div>

                                            
                                            <div class="row"  style="background-color:#346cb0;color:white;font-weight:bold; margin-top:5px">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbldisorexcesspsft" runat="server" >Discount/(Excess per SFT)</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblvaldisorexcesspsft" runat="server" clss="form-control form-control-sm "></label>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row" style="background-color:#346cb0;color:white; font-weight:bold;">
                                                <div class="col-md-8">
                                                    <div class="form-group lblmargin ">
                                                        <label id="lbltodisorexcessamt" runat="server" >Total Discount/(Excess Amt)</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">
                                                        <label id="lblvaltodisorexcessamt" runat="server" clss="form-control form-control-sm "></label>
                                                    </div>
                                                </div>

                                            </div>




                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group lblmargin textalignright">

                                                        <label id="lblDiffPV" runat="server" clss="form-control form-control-sm "></label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 10px;">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="lbtnGoalSeekRate" runat="server" CssClass=" form-control form-control-sm  btn  btn-info" OnClick="lbtnGoalSeekRate_Click">Goal Seek</asp:LinkButton>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
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

                                                        <asp:CheckBox ID="chkAddIns" runat="server" AutoPostBack="True"
                                                            CssClass="chkBoxControl"
                                                            OnCheckedChanged="chkAddIns_CheckedChanged" Text="Add.Installment" />

                                                    </div>








                                                </div>
                                                <asp:GridView ID="gvcoffsch" runat="server" CssClass=" table-striped  table-bordered grvContentarea"
                                                    AutoGenerateColumns="False" ShowFooter="True"
                                                    Style="margin-right: 0px">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvslnodumpay" runat="server"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>




                                                        <%--                                                         <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lbtnAdddsch" OnClick="lbtnAdddsch_Click" ToolTip="Add Installment"  runat="server"><span class="fa fa-plus"></span> </asp:LinkButton>


                                                            </ItemTemplate>
                                                            <ItemStyle Width="30px"  HorizontalAlign="Center"/>
                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                        </asp:TemplateField>--%>



                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lbtnDeldsch" OnClick="lbtnDeldsch_Click" ToolTip="Delete Installment" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="fa fa-trash"></span> </asp:LinkButton>


                                                            </ItemTemplate>
                                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                        </asp:TemplateField>






                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblschdesc" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="none"
                                                                    Height="16px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                                                    Width="120px"></asp:Label>



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
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy")%>'
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


                                                        <asp:TemplateField HeaderText="FV">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcofffvschamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fv")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
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

                                    <asp:Panel ID="PanelAddIns" runat="server" Visible="False">
                                        <div class="card card-fluid">
                                            <div class="card-body">
                                                <div class="row">

                                                    <div class="col-md-5">
                                                        <div class="form-group lblmargin">
                                                            <label id="lblAddinstallment" runat="server">Installement</label>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-7">
                                                        <div class="form-group lblmargin">

                                                            <asp:DropDownList ID="ddlInstallment" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="12">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div clsss="row" style="margin: 10px 0px 0px 10px;">

                                                    <div class="col-md-7 offset-5">
                                                        <div class="form-group">
                                                            <asp:LinkButton ID="lbtnAddInstallment" runat="server" CssClass=" form-control form-control-sm  btn btn-warning" OnClick="lbtnAddInstallment_Click">Add</asp:LinkButton>
                                                        </div>


                                                    </div>

                                                </div>


                                            </div>

                                        </div>
                                    </asp:Panel>

                                    <div class="row">


                                        <asp:HiddenField ID="lblhiddenbutility" runat="server" />
                                        <asp:HiddenField ID="lblhiddenbpamt" runat="server" />
                                        <asp:HiddenField ID="lblhiddenothers" runat="server" />
                                        <asp:HiddenField ID="lblhiddenbnoemi" runat="server" />
                                        <asp:HiddenField ID="lblhiddenfvpersft" runat="server" />
                                        <asp:HiddenField ID="lblhiddenpvpersft" runat="server" />
                                        <asp:HiddenField ID="lblminunitrate" runat="server" />
                                        <asp:HiddenField ID="lblhiddenncoffurate" runat="server" />

                                    </div>





                                </div>
                            </div>
                        </div>

                    </div>








                </asp:View>
            </asp:MultiView>

            <div id="modalGoalSeek" class="modal animated slideInLeft" role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="display: block;">

                            <button type="button" class="close btn btn-xs bg-danger" data-dismiss="modal">
                                <span class="fa fa-close"></span>

                            </button>
                            <h4 class="modal-title">
                                <span class="fa fa-sm fa-table pr-2" runat="server" id="txtheader">Goal Seek For Rate</span></h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row-fluid">

                                <div class="form-group" runat="server">
                                    <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lbltype" class="col-md-4">Target Difference in PV Value</asp:Label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtTargetDifference" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>



                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lnkGoalSeekExecute" runat="server" CssClass="btn btn-sm btn-success"
                                OnClientClick="CloseGoalSeek();" OnClick="lnkGoalSeekExecute_Click"><span class="glyphicon glyphicon-save"></span>Execute</asp:LinkButton>


                            <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                        </div>
                    </div>
                </div>
            </div>
            <script>
                function loadGoalSeek() {
                    $('#modalGoalSeek').modal('toggle', {
                        backdrop: 'static',
                        keyboard: false
                    });
                }
                function CloseGoalSeek() {
                    $('#modalGoalSeek').modal('hide');
                }
            </script>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





