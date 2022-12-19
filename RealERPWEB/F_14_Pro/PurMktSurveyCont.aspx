<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurMktSurveyCont.aspx.cs" Inherits="RealERPWEB.F_14_Pro.PurMktSurveyCont" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
    <style type="text/css">
        .modalcss {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: scroll;
        }

        .multiselect {
            width: 322px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 300px !important;
        }

        .multiselect-container {
            height: 350px !important;
            width: 350px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 300px !important;
        }

        .form-control {
            height: 34px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $(function () {
                $('[id*=chkMSRRes]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true,
                    //enableFiltering: true,
                });
            });
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });
            var gridview = $('#<%=this.gvMSRInfo2.ClientID %>');
            $.keynavigation(gridview);
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">

                                    <div class="form-group">
                                        <div class="col-md-3  pading5px">

                                            <asp:Label ID="Label11" runat="server" CssClass=" lblName lblTxt" Text="Survey No.:"></asp:Label>

                                            <asp:Label ID="lblCurMSRNo1" runat="server" CssClass="inputtextbox" Text="MSC00-"></asp:Label>

                                            <asp:TextBox ID="txtCurMSRNo2" runat="server" Width="66px" CssClass="inputtextbox">00000</asp:TextBox>

                                            <asp:Label ID="Label13" runat="server" CssClass=" smLbl_to" Text="Date:"></asp:Label>

                                            <asp:TextBox ID="txtCurMSRDate" runat="server" CssClass="inputtextbox" ToolTip="(dd-MMM-yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurMSRDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurMSRDate"></cc1:CalendarExtender>



                                        </div>
                                        <div class="col-md-3  pading5px">
                                            <asp:Label ID="Label2" runat="server" CssClass=" lblName lblTxt" Text="Req. No:"></asp:Label>
                                            <asp:DropDownList ID="ddlReqNo" runat="server" AutoPostBack="True" Width="250px" CssClass="ddlPage chzn-select"></asp:DropDownList>



                                        </div>
                                        <div class="col-md-3  pading5px">
                                            <asp:Label ID="Label3" runat="server" CssClass=" lblName lblTxt" Text="Projects List:"></asp:Label>
                                            <asp:DropDownList ID="ddlprjlist" runat="server" AutoPostBack="True" Width="250px" CssClass="ddlPage chzn-select"></asp:DropDownList>



                                        </div>

                                        <div class="col-md-3  pading5px">

                                            <asp:LinkButton ID="lbtnMSROk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnMSROk_Click">Ok</asp:LinkButton>

                                        </div>


                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="lblPreMrList" runat="server" CssClass=" lblName lblTxt" Text="Previous MSR:"></asp:Label>

                                            <asp:TextBox ID="txtPreMSRSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindPreMR" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindPreMR_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlPrevMSRList" runat="server" Width="322px" CssClass="ddlPage "></asp:DropDownList>

                                        </div>
                                    </div>

                                </asp:Panel>
                                <asp:Panel ID="pnlSupMat" runat="server" Visible="False">
                                    <div class="form-group">
                                        <div class="col-md-1  pading5px">
                                            <asp:Label ID="Label10" runat="server" CssClass=" lblName lblTxt" Text="Supplier List:"></asp:Label>
                                            <asp:TextBox ID="txtMSRSupSearch" runat="server" CssClass="inputtextbox hidden" Style="width: 63px;"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindSup" runat="server" CssClass="btn btn-primary primaryBtn hidden" OnClick="ImgbtnFindSup_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:DropDownList ID="ddlMSRSupl" runat="server" AutoPostBack="True" Width="322px" CssClass="ddlPage chzn-select"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="lbtnMSRSup" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnMSRSup_Click">Select Suppliers</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-1 pading5px">
                                            <asp:Label ID="Label9" runat="server" CssClass=" lblName lblTxt" Text="Materials List:"></asp:Label>
                                            <asp:TextBox ID="txtMSRResSearch" runat="server" CssClass="inputtextbox hidden" Style="width: 63px;"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindMat" runat="server" CssClass="btn btn-primary primaryBtn hidden" OnClick="ImgbtnFindMat_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:ListBox ID="chkMSRRes" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>

                                            <%--<asp:DropDownList ID="ddlMSRRes" runat="server" AutoPostBack="True" Width="322px" OnSelectedIndexChanged="ddlMSRRes_SelectedIndexChanged" CssClass="ddlPage chzn-select" Visible="false"></asp:DropDownList>--%>
                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="lbtnMSRSelect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnMSRSelect_Click">Select Materials</asp:LinkButton>
                                        </div>
                                    </div>


                                    <%--                                    <div class="form-group">
                                        <div class="col-md-1 pading5px">
                                            <asp:Label ID="lblspecificationms" runat="server" CssClass="lblTxt lblName" Text="Specification:"></asp:Label>
                                            <asp:TextBox ID="txtsrchSpecification3" runat="server" CssClass="inputTxt inpPixedWidth hidden" TabIndex="4"></asp:TextBox>

                                            <div class="colMdbtn hidden">
                                                <asp:LinkButton ID="ImgbtnFindSpecificationms" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSpecificationms_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-4 pading5px  asitCol4">
                                            <asp:DropDownList ID="ddlSpecificationms" runat="server" Width="322px" CssClass=" chzn-select  form-control inputTxt" TabIndex="6">
                                            </asp:DropDownList>
                                        </div>
                                    </div>--%>
                                </asp:Panel>
                            </div>
                        </fieldset>

                        <div class="table table-responsive" style="min-height: 360px!important">

                            <asp:GridView ID="gvMSRInfo2" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" OnPageIndexChanging="gvMSRInfo2_PageIndexChanging" PageSize="20" AllowPaging="True"
                                OnRowDataBound="gvMSRInfo2_RowDataBound" OnRowCreated="gvMSRInfo2_RowCreated">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnMrsnodelete" runat="server" OnClick="lbtnMrsnodelete_Click"><span class="glyphicon glyphicon-remove"> </span></asp:LinkButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="rsircode"  >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvrsircode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="spcfcode" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvspcfcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Materials Description ">

                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-primary  primarygrdBtn">Total</asp:LinkButton>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRResDesc" runat="server"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")                                                                         
                                                                    %>'
                                                Width="150px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Floor" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRFlrcod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Floor Desc">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRFlrdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Requirement">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvMSRqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BOQ Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvMSRbgdrat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">

                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lbtnSameValue" runat="server" OnClick="lbtnSameValue_Click" CssClass="btn btn-info primaryBtn">Put Same Rate</asp:LinkButton>
                                        </HeaderTemplate>

                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnMSRUpdate" runat="server" OnClick="lbtnMSRUpdate_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate1" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Amount">


                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamt1" runat="server" Width="70"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount1" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>





                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Rate">
                                         <HeaderTemplate>
                                            <asp:LinkButton ID="lbtnSameValueB" runat="server" OnClick="lbtnSameValueB_Click" CssClass="btn btn-info primaryBtn">Put Same Rate</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate2" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Amount">

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamt2" runat="server" Width="70"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount2" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>



                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                         <HeaderTemplate>
                                            <asp:LinkButton ID="lbtnSameValueC" runat="server" OnClick="lbtnSameValueC_Click" CssClass="btn btn-info primaryBtn">Put Same Rate</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate3" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Amount">

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamt3" runat="server" Width="70"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount3" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>



                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Rate">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lbtnSameValueD" runat="server" OnClick="lbtnSameValueD_Click" CssClass="btn btn-info primaryBtn">Put Same Rate</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate4" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamt4" runat="server" Width="70"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount4" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>



                                        <FooterStyle HorizontalAlign="RIght" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                         <HeaderTemplate>
                                            <asp:LinkButton ID="lbtnSameValueE" runat="server" OnClick="lbtnSameValueE_Click" CssClass="btn btn-info primaryBtn">Put Same Rate</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtrate5" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamt5" runat="server" Width="70"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount5" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>



                                        <FooterStyle HorizontalAlign="RIght" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Previous App.rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaprovrate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvMSRRemarks" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "msrrmrk").ToString() %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>





                            <asp:GridView ID="gvterm" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSupplierdelete" runat="server" OnClick="lbtnSupplierdelete_Click" OnClientClick="return confirm('Are you Sure to Delete This Suppliers ?');"><span class="glyphicon glyphicon-remove" > </span></asp:LinkButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Suppliercode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvssircode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Supplier">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Discount (Amt)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvDiscount" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" TextMode="Number" min="0"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Carring Charge (Amt)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvccharge" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent" TextMode="Number" min="0"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "ccharge").ToString() %>'
                                                Width="120px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Payment Term">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvpayterm" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "payterm").ToString() %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Quotation Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCurQuTDate" runat="server" CssClass="inputtextbox"
                                                Text='<%#(Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"qutdate")).Year==1900?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"qutdate")).ToString("dd-MMM-yyyy")) %>'> </asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurQuTDate_CalendarExtender" runat="server"
                                                TargetControlID="txtCurQuTDate"></cc1:CalendarExtender>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Working Duration">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtworkline" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "worktime").ToString() %>'
                                                Style="text-align: left; background-color: Transparent"
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Credit Period">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtcrPeriod" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "crperiod").ToString() %>'
                                                Style="text-align: left; background-color: Transparent"
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Notes">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNotes" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "notes").ToString() %>'
                                                Style="text-align: left; background-color: Transparent"
                                                Width="180px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>


                            <asp:Panel ID="Panel2" runat="server" Visible="False">
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label ID="lblReqNarr" runat="server" Text="Narration:" CssClass="lblName lblTxt"></asp:Label>
                                        <asp:TextBox ID="txtMSRNarr" runat="server" Width="322px" CssClass="inputtextbox" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-4 pading5px asitCol4 ">
                                            <asp:DropDownList ID="ddlrecomsup" runat="server" AutoPostBack="True" Width="322px" CssClass="ddlPage chzn-select"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>


                            </asp:Panel>




                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

