<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ShortListing.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.ShortListing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .ddllbl {
            background: white none repeat scroll 0 0 !important;
            border: 0 none !important;
            display: inline-block;
            height: 22px;
            line-height: 20px;
            width: 233px;
        }

        .lblsmll {
            padding: 3px 2px;
            margin-right: 10px;
        }

        .erroMsg {
            float: right;
            margin-top: -20px;
        }

        .txtgvCol9 {
            z-index: 999 !important;
        }

        .ajax__calendar .ajax__calendar_container {
            z-index: 999 !important;
        }

        #ContentPlaceHolder1_gvSListInfoPanelItem {
            /*height: 190px !important;*/
            position: absolute;
            z-index: 500;
            overflow: initial;
            overflow: visible !important;
        }

        /*Calendar Control CSS*/
        .cal_Theme1 .ajax__calendar_container {
            background-color: #DEF1F4;
            border: solid 1px #77D5F7;
            z-index: 9999 !important;
        }

        .cal_Theme1 .ajax__calendar_header {
            background-color: #ffffff;
            margin-bottom: 4px;
        }

        .cal_Theme1 .ajax__calendar_title,
        .cal_Theme1 .ajax__calendar_next,
        .cal_Theme1 .ajax__calendar_prev {
            color: #004080;
            padding-top: 3px;
        }

        .cal_Theme1 .ajax__calendar_body {
            background-color: #ffffff;
            border: solid 1px #77D5F7;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            color: #004080;
        }

        .cal_Theme1 .ajax__calendar_day {
            color: #004080;
            text-align: center;
        }


        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year,
        .cal_Theme1 .ajax__calendar_active {
            color: #004080;
            font-weight: bold;
            background-color: #DEF1F4;
        }

        .cal_Theme1 .ajax__calendar_today {
            font-weight: bold;
        }

        .cal_Theme1 .ajax__calendar_other,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
            color: #bbbbbb;
        }
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
            height: 58px !important;
        }

        .WrpTxt {
            white-space: normal !important;
            word-break: break-word !important;
        }
    </style>
     <script src="../../../Scripts/gridviewScrollHaVertworow.min.js"></script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

          

          <%--  var gvSListInfo = $('#<%=this.gvSListInfo.ClientID %>');


            gvSListInfo.gridviewScroll({
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
            try {

                //var gridViewScroll = new GridViewScroll({
                //    elementID: "gvSListInfo",
                //    width: 1400,
                //    height: 500,
                //    freezeColumn: true,
                //    freezeFooter: true,
                //    freezeColumnCssClass: "GridViewScrollItemFreeze",
                //    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                //    freezeHeaderRowCount: 1,
                //    freezeColumnCount: 8,

                //});

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
                gvSListInfo.gridviewScroll({
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
                });
                gridViewScroll.enhance();
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e) {
              
            }


            function calendarShown(sender, args) {
                sender._popupBehavior._element.style.zIndex = 1000;
            }

            //$('.chzn-select').chosen({ search_contains: true });

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

                        <div class="col-lg-3 col-md-4 col-sm-6 ">
                            <div class="form-group">
                                <asp:Label ID="lblpreAdv" runat="server">ADV List
                                   <asp:LinkButton ID="ImgbtnFindReq" runat="server" CssClass="fas fa-search" OnClick="ImgbtnFindReq_Click"></asp:LinkButton></asp:Label>
                                <asp:TextBox ID="txtSrchPre" runat="server" CssClass="form-control chzn-select d-none"></asp:TextBox>
                                <%--<asp:LinkButton ID="ImgbtnFindReq" runat="server" CssClass="fas fa-search" OnClick="ImgbtnFindReq_Click"></asp:LinkButton>--%>
                                <asp:DropDownList ID="ddlPrevAdvList" runat="server" CssClass="form-control chzn-select" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlPrevAdvList_SelectedIndexChanged"></asp:DropDownList>
                                <asp:Label ID="lblPreAdvlist" runat="server" Visible="False"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 ">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server">Date</asp:Label>
                                <asp:TextBox ID="txtCurAdvDate" runat="server" CssClass=" form-control "></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurAdvDate"></ajaxToolkit:CalendarExtender>
                                <asp:Label ID="Label12" runat="server" Font-Bold="True" CssClass="d-none"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblResList" runat="server">Post List</asp:Label>
                                <asp:TextBox ID="txtPostSearch" runat="server" CssClass="form-control d-none"></asp:TextBox>
                                <asp:LinkButton ID="ImgbtnFindPost" runat="server" CssClass="fas fa-search" OnClick="ImgbtnFindPost_Click"></asp:LinkButton>

                                <asp:DropDownList ID="ddlPOSTList" runat="server" Width="180px" CssClass="form-control chzn-select " TabIndex="2"></asp:DropDownList>
                                <asp:Label ID="lblPostList" runat="server" Visible="False"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-1 mt-3 col-md-2 col-sm-6 ">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary" OnClick="lbtnOk_Click">ok</asp:LinkButton>
                            </div>
                        </div>


                       <%-- <div class="col-lg-1">
                            <div class="form-group">
                                <asp:Panel ID="PanelAddCan" runat="server">
                                    <asp:LinkButton ID="lbtnAddList0" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnAddList_Click">Add Row</asp:LinkButton>
                                    <asp:Label ID="lblslnum" runat="server" Visible="False"></asp:Label>
                                </asp:Panel>
                            </div>
                        </div>--%>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" Visible="false" CssClass="smLbl_to">Prev</asp:Label>

                                <asp:LinkButton ID="lbkButtonPrev" Visible="false" runat="server" CssClass="fas fa-search" OnClick="lbkButtonPrev_Click"></asp:LinkButton>

                                <asp:DropDownList ID="ddlPrevlist" Visible="false" runat="server" Width="160px" CssClass="form-control chzn-select" TabIndex="2" OnSelectedIndexChanged="ddlPrevlist_SelectedIndexChanged"></asp:DropDownList>


                                <asp:Label ID="lblmsg1" runat="server" Visible="false" CssClass="btn btn-success primaryBtn pull-right"></asp:Label>
                            </div>
                        </div>

                         <div class="col-lg-1 mt-3">
                            <div class="form-group">
                                <asp:Panel ID="PanelAddCan" runat="server">
                                    <asp:LinkButton ID="lbtnAddList0" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnAddList_Click">Add Row</asp:LinkButton>
                                    <asp:Label ID="lblslnum" runat="server" Visible="False"></asp:Label>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>


                <%-- <..........................................................>--%>
                <fieldset class="scheduler-border fieldset_A" style="background: #f9f99c">
                    <div class="form-horizontal">
                        <asp:Panel ID="Pnladv" runat="server">
                            <div class="form-group">

                                <div class="col-md-12 pading2px">
                                    <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName"><strong>Department: </strong></asp:Label>
                                    <asp:Label ID="lblDpt" runat="server" CssClass="smLbl_to lblsmll"></asp:Label>

                                    <asp:Label ID="Label11" runat="server" CssClass="smLbl_to">Post :</asp:Label>
                                    <asp:Label ID="lblPost" runat="server" CssClass="smLbl_to lblsmll">""</asp:Label>

                                    <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">Qualification:</asp:Label>
                                    <asp:Label ID="lblQua" runat="server" CssClass="smLbl_to lblsmll">""</asp:Label>


                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Experience :</asp:Label>
                                    <asp:Label ID="lblexp" runat="server" CssClass="smLbl_to lblsmll">""</asp:Label>



                                    <asp:Label ID="Label2" runat="server" CssClass="smLbl_to">Salary Range :</asp:Label>
                                    <asp:Label ID="lblsal" runat="server" CssClass="smLbl_to lblsmll">""</asp:Label>

                                    <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName">Num.of Position :</asp:Label>
                                    <asp:Label ID="lblnpos" runat="server" CssClass=" smLbl_to lblsmll">""</asp:Label>

                                    <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Approve By :</asp:Label>
                                    <asp:Label ID="lblapp" runat="server" CssClass="smLbl_to lblsmll">""</asp:Label>


                                </div>




                            </div>
                        </asp:Panel>
                    </div>
                </fieldset>
                <%-- <..........................................................>--%>
            </div>

            <div class="card-body">
                <div class="row">
                    <asp:Panel ID="Panel2" runat="server">


                        <asp:GridView ID="gvSListInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            ShowFooter="True" PageSize="15" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvSListInfo_RowDataBound"
                            OnRowDeleting="gvSListInfo_RowDeleting" OnPageIndexChanging="gvSListInfo_PageIndexChanging" OnRowCommand="gvSListInfo_RowCommand">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0a" runat="server" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="40" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Post Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPostCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl Number" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvissue" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "listisu")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col1">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdateResReq" runat="server" Font-Bold="True" CssClass="btn btn-success primaryBtn" OnClick="lbtnUpdateResReq_Click" Style="text-align: center;"
                                            Width="120px">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol1" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BackColor="#dadada"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col1").ToString() %>' Width="120px"></asp:TextBox>


                                        <asp:RequiredFieldValidator CssClass="erroMsg"
                                            ControlToValidate="txtgvCol1"
                                            runat="server" ForeColor="Red"
                                            ErrorMessage="*"
                                            Text="" />

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col2">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol2" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col2").ToString() %>' Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col3">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol3" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col3").ToString() %>' Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col4">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol4" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col4").ToString() %>' Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col5">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol5" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col5").ToString() %>' Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col6">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol6" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col6").ToString() %>' Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col7">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol7" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BackColor="#dadada"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col7").ToString() %>' Width="50px"></asp:TextBox>

                                        <%-- <asp:RequiredFieldValidator CssClass="erroMsg"
                                                ControlToValidate="txtgvCol7"
                                                runat="server" ForeColor="Red"
                                                ErrorMessage="*"
                                                Text="" />--%>
                                    </ItemTemplate>






                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col8">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol8" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col8").ToString() %>' Width="50px"></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col9" SortExpression="col9">
                                    <ItemTemplate>


                                        <%--  <asp:TextBox ID="txtgvCol9" type="date" dateformat="d M y" runat="server"
                                                Text='<%# Bind("col9", "{0:dd-MMM-yyyy}") %>' CssClass="inputDateBox" Width="120px" Style="line-height: 12px;"></asp:TextBox>--%>

                                        <%-- <asp:TextBox ID="txtgvCol9" runat="server" CssClass="txtboxformat" Width="110px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvCol9_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtgvCol9"></cc1:CalendarExtender>--%>

                                        <%--            <asp:TextBox SkinID="txtboxCustomizedMfSkin" ID="txtgvCol9" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "col9")).ToString("dd-MMM-yyyy")) %>'  runat="server" Width="80px" CausesValidation="true"  />
                                            
      
                                           <ajaxToolkit:CalendarExtender  Format="dd-MMM-yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtgvCol9" CssClass="cal_Theme1" />
                                        --%>
                                        <asp:TextBox ID="txtgvCol9" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col9").ToString() %>' Width="70px"></asp:TextBox>



                                        <%--   <asp:TextBox SkinID="txtboxCustomizedMSkin" ID="txtgvCol9" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "col9")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "col9")).ToString("dd-MMM-yyyy")) %>' runat="server" CausesValidation="true" ReadOnly="true" />
                                                <ajaxToolkit:CalendarExtender  Format="dd-MMM-yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtgvCol9" CssClass=" cal_Theme1" />
                                        --%>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col10">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol10" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BackColor="#dadada"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left;" Width="50px"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col10").ToString() %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col11">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol11" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BackColor="#dadada"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col11").ToString() %>' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col12">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol12" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BackColor="#dadada"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; margin: 0 1px;"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col12").ToString() %>' Width="60px" CausesValidation="True"></asp:TextBox>


                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="!!" Style="float: right;"
                                                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtgvCol12" ForeColor="Red" />--%>

                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red"
                            ControlToValidate="txtgvCol12" />--%>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col13">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol13" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BackColor="#dadada"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left;"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col13").ToString() %>' Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col14">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol14" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BackColor="#dadada"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left;"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col14").ToString() %>' Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Col15">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol15" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right;"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col15").ToString() %>' Width="50px"></asp:TextBox>



                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- Total Mark --%>
                                <asp:TemplateField HeaderText="Total Mark">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltomark" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tomark")).ToString("#,##0.00;(#,##0.00);") %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col16">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol16" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col16").ToString() %>' Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col17">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol17" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BackColor="#dadada"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col17").ToString() %>' Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col18">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol18" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BackColor="#dadada"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col18").ToString() %>' Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Col19">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol19" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BackColor="#dadada"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col19").ToString() %>' Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Col20" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCol20" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BackColor="#dadada"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "col20").ToString() %>' Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Selection">
                                    <ItemTemplate>



                                        <asp:CheckBox ID="chkvslno" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkvno"))=="True" %>'
                                            Width="20px" />
                                        <asp:LinkButton ID="lbSelection" runat="server" Width="60px"
                                            OnClick="lbSelection_Click">Selection</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="90px" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CV">
                                    <ItemTemplate>
                                        <%--<asp:HyperLink runat="server" ID="hyplink" CssClass="block" Text='<%# DataBinder.Eval(Container.DataItem, "col18").ToString() %>'></asp:HyperLink>--%>
                                        <asp:HyperLink ID="hlnkcv" runat="server" Target="_blank" NavigateUrl='<%# Eval("col20") %>'>
                                             <span><%# Eval("cvname") %></span>
                                        </asp:HyperLink>


                                        <asp:Label ID="lblcv" runat="server" Height="16px" Style="display: none" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col20")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>


                                    <ItemStyle Width="80px" />
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table style="width: 30px;">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="chkall_CheckedChanged" />
                                                    SMS

                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSMSEmail" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sendflag"))=="True" %>'
                                            Width="30px" />
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>




                        <div class="row">

                            <div class="col-md-5 col-sm-5 col-lg-5">
                                <asp:Panel ID="PanelAddInt" runat="server">

                                    <div class="form-group">
                                        <label class="btn btn-sm btn-success">Interviewer Select</label>
                                    </div>


                                    <div class="form-group">


                                        <asp:Label ID="Label3" runat="server" Visible="false" CssClass=" lblTxt lblName">Select</asp:Label>
                                        <asp:TextBox ID="txtIntList" runat="server" CssClass="form-control  chzn-select d-none"></asp:TextBox>
                                        <asp:LinkButton ID="imgBtnInt" runat="server" CssClass="fas fa-search" OnClick="imgBtnInt_Click"></asp:LinkButton>


                                        <asp:DropDownList ID="ddlInterviewer" runat="server" Width="233" CssClass="form-control  chzn-select" TabIndex="2" AutoPostBack="true">
                                        </asp:DropDownList>

                                        <asp:LinkButton ID="lbtnSelectRes" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnSelectRes_Click" Width="70">Select</asp:LinkButton>
                                        <asp:Label ID="lblmsg2" runat="server" Visible="false" CssClass="btn btn-success primaryBtn pull-right" Style="margin-right: 50px;"></asp:Label>


                                    </div>
                                </asp:Panel>

                                <asp:GridView ID="gvIntInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="16px" PageSize="15" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDeleting="gvIntInfo_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Postcode Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPostCoda" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postcode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Int Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIntCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intcode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name of Interviewer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIntDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intdesc"))   %>'
                                                    Width="150px">
                                                            
                                                            
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date of Interview">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdateInt" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false" OnClick="lbtnUpdateInt_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <%-- <asp:TextBox ID="txtgvIntDat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" Visible="false"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intdat")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intdat")).ToString("dd-MMM-yyyy")) %>' Width="80px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvIntDat_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvIntDat"></cc1:CalendarExtender>--%>
                                                <%--  <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="537px"></asp:TextBox>--%>

                                                <asp:TextBox ID="txtgvIntDat" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intdat")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intdat")).ToString("dd-MMM-yyyy")) %>' runat="server" CausesValidation="true" BorderStyle="None" />
                                                <%-- <ajaxToolkit:CalendarExtender  Format="dd-MMM-yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtgvIntDat" CssClass=" cal_Theme1" />--%>
                                                <cc1:CalendarExtender ID="txtgvIntDat_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvIntDat" PopupPosition="TopRight"></cc1:CalendarExtender>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRem" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>' Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                            <div class="col-md-4 col-sm-4 col-lg-4">

                                <div id="contact-form" class="contact-form">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="btn btn-sm btn-success">SMS/EMAIL Content</label>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <textarea class="form-control textarea" runat="server" rows="3" name="Message" id="Message" placeholder="Message">DEAR CANDIDATE,
YOU HAVE BEEN INVITED FOR A WRITTEN EXAM ON</textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:LinkButton ID="lnkSendsmsemail" runat="server" CssClass="btnsave btncommon btn main-btn pull-right" Style="margin: 0 5px;" OnClick="lnkSendsmsemail_Click"><i class="fa fa-paper-plane" aria-hidden="true"></i>  Send SMS/Email</asp:LinkButton>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3">
                                <asp:Panel ID="pnlcv" runat="server" Visible="false">

                                    <div id="contact-forms" class="contact-form">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="btn btn-sm btn-success">Candidate CV Upload</label>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlCandidate" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>

                                                    <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                        OnClientUploadComplete="uploadComplete" runat="server"
                                                        ID="AsyncFileUpload1" Width="150px" UploaderStyle="Modern"
                                                        CompleteBackColor="White"
                                                        UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                        OnUploadedComplete="FileUploadComplete" />
                                                    <asp:Image ID="imgLoader" runat="server" ImageUrl="~/images/loader.gif" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>



                        </div>
                        <div class="row">
                        </div>
                    </asp:Panel>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function uploadComplete(sender) {
            $get("<%=lblmsg1.ClientID%>").style.color = "green";
            $get("<%=lblmsg1.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblmsg1.ClientID%>").style.color = "red";
            $get("<%=lblmsg1.ClientID%>").innerHTML = "File upload failed.";
        }


    </script>
</asp:Content>

