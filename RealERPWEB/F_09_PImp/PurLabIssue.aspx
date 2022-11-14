<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PurLabIssue.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurLabIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script src="../../../Scripts/gridviewScrollHaVertworow.min.js"></script>


    <!-- Include the plugin's CSS and JS: -->
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />
    <style>
        .multiselect  {
            width:300px !important;
           border: 1px solid;
            height: 29px;
            border-color: #cfd1d4;
            font-family: sans-serif;
           
        }
        .multiselect-container{
            overflow: scroll;
            max-height: 300px !important;
        }
        /*.multiselect {
            width: 270px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }*/

        .multiselect-text {
            width: 200px !important;
        }

        /*.multiselect-container {
            height: 250px !important;
            width: 300px !important;
            overflow-y: scroll !important;
        }*/
        .caret {
            display: none !important;
        }
        span.multiselect-selected-text {
            width: 200px !important;
        }

        #ContentPlaceHolder1_divgrp {
            /*width: 395px !important;*/
        }

        .form-control {
            height: 34px;
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
 
    </style>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

           // var gvisu = $('#<%=this.grvissue.ClientID %>');
            //$.keynavigation(gvisu);
            // gvisu.Scrollable();
           // $('.chzn-select').chosen({ search_contains: true });

            $(function () {
                $('[id*=DropCheck1]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true,
                });
            });

            //gvisu.gridviewScroll({
            //    width: 1265,
            //    height: 420,
            //    arrowsize: 30,
            //    railsize: 16,
            //    barsize: 8,
            //    headerrowcount: 2,
            //    varrowtopimg: "../Image/arrowvt.png",
            //    varrowbottomimg: "../Image/arrowvb.png",
            //    harrowleftimg: "../Image/arrowhl.png",
            //    harrowrightimg: "../Image/arrowhr.png",
            //    freezesize: 5
            //});

            var gridViewScroll = new GridViewScroll({
                elementID: "grvissue",
                width: 1265,
                height: 300,
                freezeColumn: true,
                freezeFooter: true,
                freezeColumnCssClass: "GridViewScrollItemFreeze",
                freezeFooterCssClass: "GridViewScrollFooterFreeze",
                freezeHeaderRowCount: 1,
                freezeColumnCount: 5,
            });

            gridViewScroll.enhance();
            $('.chzn-select').chosen({ search_contains: true });
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
            <div class="card card-fluid mt-4">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label ID="Label3" runat="server" CssClass="form-label">Bill ID:</asp:Label>
                            <asp:Label ID="lblCurISSNo1" runat="server" CssClass="inputTxt  inputBox50px" Text="LIS00-"></asp:Label>
                            <asp:TextBox ID="txtCurISSNo2" runat="server" CssClass="form-control form-control-sm  w100">000</asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label4" runat="server" CssClass="form-label">R/A No</asp:Label>
                            <asp:DropDownList ID="ddlRA" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlRA_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="Label5" runat="server" CssClass="form-label">Ref No</asp:Label>
                            <asp:TextBox ID="txtRefno" runat="server" CssClass="form-control form-control-sm  w100"></asp:TextBox>
                        </div>
                        <div class="col-sm-12 col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="form-label">Date</asp:Label>
                                <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="form-control form-control-sm  w100"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1_txtCurISSDate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="Label2" runat="server" CssClass="form-label">Page Size</asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                              
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>600</asp:ListItem>
                                <asp:ListItem>1000</asp:ListItem>
                                <asp:ListItem>2000</asp:ListItem>
                                <asp:ListItem>3000</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 col-md-3">
                            <div class="form-group">
                                <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="form-label" OnClick="ibtnFindProject_Click">Project Name</asp:LinkButton>
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputDateBox" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-3">
                            <div class="form-group">
                                <asp:LinkButton ID="ibtnFindSubConName" runat="server" CssClass="form-label" OnClick="ibtnFindSubConName_Click">Contractor List</asp:LinkButton>
                                <asp:TextBox ID="txtSrcSub" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlcontractorlist" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%-- <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="form-label"></asp:Label>
                                <asp:LinkButton ID="" runat="server" CssClass="btn btn-primary" OnClick="">Ok</asp:LinkButton>
                            </div>
                        </div>--%>
                        <div class="col-sm-12 col-md-3 ">
                            <div class="form-group">
                               
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary" style="margin-top:20px;" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnPrevISSList" runat="server" CssClass="form-label" OnClick="lbtnPrevISSList_Click">Prev. List</asp:LinkButton>
                                <asp:LinkButton ID="ibtnPreBillList" runat="server" CssClass="form-label" OnClick="ibtnPreBillList_Click" Visible="false"></asp:LinkButton>

                                <asp:TextBox ID="txtSrcPreBill" runat="server" CssClass="inputTxt inputDateBox" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>


                    </div>

                </div>


                <div class="card-header" id="PnlRes" runat="server" visible="false">
                    <div class="row">
                        <div class="col-sm-12 col-md-2">
                            <div class="form-group" id="divgrp" runat="server">
                                <asp:Label ID="lblgrp" runat="server" CssClass="form-label" Visible="false" ClientIDMode="Static">Group</asp:Label>
                                <asp:DropDownList ID="ddlgroup" runat="server" CssClass="form-control chzn-select" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblfloorno" runat="server" CssClass="form-label">Floor No</asp:Label>
                                <asp:DropDownList ID="ddlfloorno" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlfloorno_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Selected="True" Text="Unspecified" Value="00"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblCatagory" runat="server" CssClass="form-label">Catagory</asp:Label>
                                <asp:DropDownList ID="ddlcatagory" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlcatagory_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="form-label">Labour

                                <asp:LinkButton ID="ibtnSearchMaterisl" runat="server" CssClass="form-label" 
                                    OnClick="ibtnSearchMaterisl_Click"><i class="fa fa-search"> </i></asp:LinkButton>
                                     <asp:CheckBox ID="chkCharging" runat="server" AutoPostBack="True" style="margin-left:30px;"
                                    OnCheckedChanged="chkCharging_CheckedChanged" Text="Charging" ForeColor="Green" Visible="false" />
                                </asp:Label>
                                <asp:TextBox ID="txtSearchLabour" runat="server" CssClass="inputTxt inputDateBox" Visible="false"></asp:TextBox>
                                
                                <div class="col-md-3 pl-0">

                                    <asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control" Style="min-width: 100px !important;" SelectionMode="Multiple"></asp:ListBox>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary" style="margin-top:20px;" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                            </div>
                        </div>
                        
                    </div>

                    <div class="row" style="min-height:200px; background-color:whitesmoke">
                        <asp:GridView ID="grvissue" runat="server" ClientIDMode="Static" 
                                        CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="grvissue_RowDataBound"
                            OnRowDeleting="grvissue_RowDeleting" OnPageIndexChanging="grvissue_PageIndexChanging"> 
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ControlStyle-Width="20px" ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText='<span class="fa fa-sm fa-trash fa" aria-hidden="true" ></span>&nbsp;' />


                                <asp:TemplateField HeaderText="Item Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemcode" runat="server" Width="20px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Fl" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvflrCode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Floor Desc.">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnDeleteBill" runat="server" Font-Bold="True"
                                            Font-Size="13px" ForeColor="#000" OnClick="lbtnDeleteBill_Click"
                                            Style="text-align: center;" Width="90px" CssClass="btn btn-primary primarygrdBtn">Delete All</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFloordes" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description">

                                    <ItemTemplate>
                                        <asp:Label ID="gvlblgrp" runat="server" CssClass="d-none"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grp")) %>'></asp:Label>
                                        <asp:Label ID="lbllabdesc" runat="server" style="word-wrap: break-word"
                                            Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "")   %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkTotal" runat="server" Font-Bold="True" Font-Size="14px" Width="40px"
                                            ForeColor="#000" OnClick="lnkTotal_Click" CssClass="btn btn-primary btn-sm">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label14" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pre. Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpreqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="MB Book">

                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtmbbook" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mbbook")) %>'
                                            Width="70px" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: left"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Budgeted Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbgdqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Budgeted Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblschrate" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Work Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblwrkrate" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkrate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>







                                <asp:TemplateField HeaderText="Bal.Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbalqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="65px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Issued % on Budget">
                                    <ItemTemplate>
                                        <asp:Label ID="lblperobgd" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronbgd")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Work Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtwrkqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Balance Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbalamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>






                                <asp:TemplateField HeaderText="%">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtpercentge" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Previous Rate" Visible="False">


                                    <ItemTemplate>
                                        <asp:Label ID="lblgvprerate" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prevrat")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                            Width="70px" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Qty">

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="lnkupdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtisuqty" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                            Width="60px" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total Qty" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtoisuqty" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toqty")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                            Width="70px" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Rate">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtlabrate" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isurat")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="60px" BackColor="Transparent"
                                            BorderStyle="None" BorderWidth="1px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>






                                <asp:TemplateField HeaderText="Amount" Visible="false">


                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvamount" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFamount" runat="server" Style="text-align: right"
                                            Width="70px" Font-Size="12px" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>




                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Ded. Qty" Visible="false">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdedqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="50px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Ded. Unit" Visible="false">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdedunit" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dedunit")) %>'
                                            Width="50px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Ded. Rate" Visible="false">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdedrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedrate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="50px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Ded. Amount" Visible="false">

                                    <FooterTemplate>
                                        <asp:Label ID="lblFidedamt" runat="server" Style="text-align: right"
                                            Width="70px" Font-Size="12px" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdedamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "idedamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="50px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText=" Bill Amount After Deduction" Visible="false">


                                    <ItemTemplate>
                                        <asp:Label ID="lblgvadedamount" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adedamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFadedamount" runat="server" Style="text-align: right"
                                            Width="70px" Font-Size="12px" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>




                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>






                                <asp:TemplateField HeaderText="Above" Visible="false">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvabove" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "above")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="50px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Bill Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblFissueamt" runat="server" Style="text-align: right"
                                            Width="70px" Font-Size="12px" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtissueamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                            Width="70px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
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

                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label ID="lblsecurity" runat="server" CssClass="form-label">Security Deposit</asp:Label>
                            <asp:TextBox ID="txtpercentage" runat="server" CssClass="form-control form-control-sm  w100"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnDepost" runat="server" CssClass="form-label" OnClick="lbtnDepost_Click">
                                Calculation Amount <i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                            <asp:TextBox ID="txtSDAmount" runat="server" CssClass="form-control form-control-sm  w100"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lbldeduction" runat="server" CssClass="form-label">Deduction</asp:Label>
                            <asp:TextBox ID="txtDedAmount" runat="server" CssClass="form-control form-control-sm  w100"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblpenalty" runat="server" CssClass="form-label">Penalty</asp:Label>
                            <asp:TextBox ID="txtPenaltyAmount" runat="server" CssClass="form-control form-control-sm  w100"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblAdvanced" runat="server" CssClass="form-label">Advanced</asp:Label>
                            <asp:TextBox ID="txtAdvanced" runat="server" CssClass="form-control form-control-sm  w100"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblReward" runat="server" CssClass="form-label">Reward</asp:Label>
                            <asp:TextBox ID="txtreward" runat="server" CssClass="form-control form-control-sm  w100"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblnettotal" runat="server" CssClass="form-label">Net Total</asp:Label>
                            <asp:TextBox ID="lblvalnettotal" runat="server" CssClass="form-control form-control-sm  w100" Enabled="false" Style="text-align: right; color: blue;"></asp:TextBox>
                            <asp:HyperLink ID="lbtnBalance" runat="server" Target="_blank" Style="margin-left: 10px; color: blue; font-weight: bold; font-size: 14px;"></asp:HyperLink>

                        </div>
                    </div>
                    <div class="row" id="PnlNarration" runat="server">
                        <div class="col-sm-12 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="form-label">Naration</asp:Label>
                                <asp:TextBox ID="txtISSNarr" runat="server" CssClass="form-control form-control-sm " Height="100px" TextMode="MultiLine"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-sm-12 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblTrade" runat="server" CssClass="form-label">Trade</asp:Label>
                                <asp:DropDownList ID="ddltrade" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-12">
                                <asp:Label ID="lblPreparedBy" runat="server" CssClass="lblTxt lblName" Text="Prepared By:" Visible="False"></asp:Label>
                                <asp:TextBox ID="txtPreparedBy" runat="server" Visible="False" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                <asp:Label ID="lblApprovedBy" runat="server" CssClass="lblTxt lblName" Text="Approved By:" Visible="False"></asp:Label>
                                <asp:TextBox ID="txtApprovedBy" runat="server" Visible="False" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                <asp:Label ID="lblApprovalDate" runat="server" CssClass="lblTxt lblName" Text="Approv.Date:" Visible="False"></asp:Label>
                                <asp:TextBox ID="txtApprovalDate" runat="server" Visible="False" CssClass="inputTxt inputDateBox"></asp:TextBox>
                            </div>
                        </div>


                    </div>
                </div>



                <asp:Label ID="lblBillno" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>

            </div>






        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

