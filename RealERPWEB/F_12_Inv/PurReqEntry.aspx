<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurReqEntry.aspx.cs" Inherits="RealERPWEB.F_12_Inv.PurReqEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


            var gridview = $('#<%=this.gvReqInfo.ClientID %>');
            $.keynavigation(gridview);
            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });




        };


        //function loadModal() {
        //    $('#detialsinfo').modal('toggle');
        //}

        function CloseModal() {
            $('#detialsinfo').modal('hide');


        }



        function Confirmation() {
            if (confirm('Are you sure you want to save?')) {
                return;
            } else {
                return false;
            }
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
                        <asp:Panel ID="Panel3" runat="server">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="row">

                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                                    <asp:TextBox ID="txtProjectSearch" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>
                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="ImgbtnFindProjectName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProjectName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                    </div>


                                                </div>
                                                <div class="col-md-4 pading5px asitCol4">
                                                    <div class="ddlListPart">
                                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control inputTxt chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" TabIndex="3" Style="width: 313px;"></asp:DropDownList>
                                                    </div>
                                                    <asp:Label ID="lblddlProject" runat="server" Visible="False" CssClass="inputlblVal" Style="width: 313px;"></asp:Label>


                                                </div>
                                                <div class="col-md-3 pading5px asitCol3">

                                                    <asp:Label ID="lblCurDate" runat="server" CssClass="lblTxt lblDate " Text="Req.Date"></asp:Label>

                                                    <asp:TextBox ID="txtCurReqDate" runat="server" CssClass="inputtextbox" TabIndex="5" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server"
                                                        Format="dd.MM.yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>

                                                    <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn" TabIndex="4"></asp:LinkButton>

                                                </div>
                                                <div class="col-md-3 pading5px">
                                                    <asp:Label ID="lblLastReqNo4" CssClass="" runat="server"></asp:Label>


                                                </div>
                                                <div class="col-md-3 pading5px asitCol3 pull-right">

                                                    <asp:LinkButton ID="lbtnSurVey" runat="server" CssClass="btn btn-primary primaryBtn" Visible="False">Survey</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="lblCurNo" runat="server" CssClass="lblTxt lblName" Text="Requisition No."></asp:Label>
                                                    <asp:TextBox ID="txtReqText" runat="server" TabIndex="5" CssClass="inputtextbox"></asp:TextBox>

                                                    <asp:LinkButton ID="ImgbtnReqse" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnReqse_Click" TabIndex="6"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>
                                                <div class="col-md-4 pading5px asitCol4">
                                                    <asp:Label ID="lblCurReqNo1" runat="server" CssClass="smLbl" Text="REQ00"></asp:Label>

                                                    <asp:TextBox ID="txtCurReqNo2" runat="server" CssClass="xsDropDow inputTxt disabled readonlyValue" ReadOnly="True" TabIndex="8">00000</asp:TextBox>
                                                    <asp:Label ID="lblmrfno" runat="server" CssClass=" smLbl_to" Text="M.R.F. No."></asp:Label>
                                                    <asp:TextBox ID="txtMRFNo" runat="server" TabIndex="7" CssClass="inputtextbox" Style="width: 120px;"></asp:TextBox>
                                                </div>





                                                <div class="col-md-3 pading5px">

                                                    <asp:CheckBox ID="chkdupMRF" runat="server" Text="Dup.M.R.F" CssClass="btn btn-primary checkBox" Visible="false"
                                                        TabIndex="9" />
                                                    <asp:CheckBox ID="chkneBudget" runat="server" Text="Not Exceed Budget" CssClass="btn btn-primary checkBox" Visible="false"
                                                        TabIndex="10" />

                                                </div>


                                            </div>
                                        </div>

                                        <div class="col-md-4 pading5px pull-right">
                                            <div class="form-group">
                                                <asp:Label ID="lblpreReq" runat="server" CssClass=" smLbl_to" Text="Pre Req.List"></asp:Label>
                                                <asp:TextBox ID="txtSrchMrfNo" runat="server" TabIndex="7" CssClass=" inputtextbox"></asp:TextBox>
                                                <asp:LinkButton ID="ImgbtnFindReq" runat="server" CssClass="btn btn-primary srearchBtn" Style="float: left;" OnClick="ImgbtnFindReq_Click" TabIndex="10"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                <%-- <asp:ListBox ID="ddlPrevReqList" runat="server" Height="100" CssClass="smDropDown";"></asp:ListBox>--%>

                                                <asp:DropDownList ID="ddlPrevReqList" runat="server" Style="width: 200px;" CssClass="chzn-select smDropDown inputTxt" TabIndex="11">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>





                                </div>
                            </fieldset>
                        </asp:Panel>
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">

                                    <div class="form-group">

                                        <div class="col-md-3 pading5px  asitCol3">
                                            <asp:Label ID="lblCatagory" runat="server" CssClass="lblTxt lblName" Text="Catagory"></asp:Label>
                                            <asp:TextBox ID="txtResSearch" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImgbtnFindRes" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindRes_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-2 pading5px asitCol2">
                                            <asp:DropDownList ID="ddlCatagory" runat="server" CssClass=" ddlPage chzn-select" Style="width: 150px;" AutoPostBack="True" OnSelectedIndexChanged="ddlCatagory_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:Label ID="lblResList" runat="server" CssClass="smLbl_to" Text="Materials List" Style="float: right"></asp:Label>
                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <asp:DropDownList ID="ddlResList" runat="server" CssClass="ddlPage chzn-select" Style="width: 200px;" AutoPostBack="True" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 pading5px    asitCol3">
                                            <a class="btn btn-primary srearchBtn pull-left" data-toggle="modal" data-target="#detialsinfo"><span class="glyphicon glyphicon-plus"></span>
                                            </a>
                                            <%--<asp:LinkButton ID="lbtnAddspecifiation" CssClass="btn btn-primary srearchBtn pull-left" runat="server" OnClick="lbtnAddspecifiation_Click" TabIndex="2"><span class="glyphicon   glyphicon-plus"> </span></asp:LinkButton>--%>

                                            <asp:Label ID="lblSpecification" runat="server" CssClass=" smLbl_to  pull-left" Text="Specf"></asp:Label>

                                            <%--   <asp:TextBox ID="txtSrchSpecification" runat="server" CssClass="inputtextbox" TabIndex="5"></asp:TextBox>

                                            <asp:LinkButton ID="ImgbtnSpecification" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnSpecification_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>--%>


                                            <%-- <cc1:ListSearchExtender ID="ListSearchExt1" runat="server"
                                                QueryPattern="Contains" TargetControlID="ddlResList">
                                            </cc1:ListSearchExtender>--%>
                                            <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass=" ddlPage chzn-select" TabIndex="3" Width="100px"></asp:DropDownList>
                                        </div>


                                        <div class="col-md-1">
                                            <asp:LinkButton ID="lbtnSelectRes" runat="server" OnClick="lbtnSelectRes_Click" CssClass="btn btn-primary primarygrdBtn" Style="line-height: 15px;" Height="25">Select</asp:LinkButton>
                                        </div>


                                        <asp:Label ID="lblfloor" runat="server" CssClass="col-md-1 label pading5px" Visible="False"></asp:Label>
                                        <asp:Label ID="lblddlFloor" runat="server" CssClass="col-md-1 label pading5px" Visible="False"></asp:Label>
                                        <div class="ddlListPart">
                                            <asp:DropDownList ID="ddlFloor" runat="server" AutoPostBack="True" TabIndex="15" Visible="False" Width="120px"></asp:DropDownList>
                                        </div>

                                    </div>




                                </div>
                            </fieldset>



                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server" Visible="False">


                            <div class="table-responsive">
                                <asp:GridView ID="gvReqInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowCancelingEdit="gvReqInfo_RowCancelingEdit" OnRowDeleting="gvReqInfo_RowDeleting" OnRowEditing="gvReqInfo_RowEditing" OnRowUpdating="gvReqInfo_RowUpdating" PageSize="15" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Visible="False" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:CommandField ShowDeleteButton="True" />--%>

                                        <asp:TemplateField ShowHeader="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDelMat" runat="server" OnClick="lbtnDelMat_Click" ToolTip="Delete Material" OnClientClick="javascript:return FunConfirm();"><span class="glyphicon glyphicon-trash" style="color:red"> </span> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True" Font-Bold="True" Font-Size="14px" OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged" Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid" Width="140px">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server"
                                                    Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                                    Width="140px">
                                                            
                                                            
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Present Stock In Store">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstkqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.000;-#,##0.000; ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBgdBal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bbgdqty")).ToString("#,##0.000;-#,##0.000; ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnResFooterTotal" runat="server" Font-Bold="True" OnClick="lbtnResFooterTotal_Click" CssClass="btn btn-primary  primarygrdBtn">Total :</asp:LinkButton>

                                                <%-- <asp:LinkButton ID="lbtnUnAprov" runat="server" Font-Bold="True" onclick="lbtnUnAprov_Click"  CssClass="btn btn-primary  primarygrdBtn">Un Aproved</asp:LinkButton>--%>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Present </br> Requirement">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvReqQty" runat="server" BorderColor="#99CCFF" BackColor="Wheat" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.000;(#,##0.000); ") %>' Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:CheckBox ID="crChkbox" runat="server" Visible="false" Text="CRM Checkd" />

                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" BackColor="#69AEE7" />
                                            <HeaderStyle ForeColor="Blue" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site Supply Date">
                                            <FooterTemplate>


                                                <asp:LinkButton ID="lbtnUpdateResReq" runat="server" OnClick="lbtnUpdateResReq_Click" CssClass="btn  btn-danger primarygrdBtn">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvUseDat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent" Text='<%# DataBinder.Eval(Container.DataItem, "expusedt").ToString() %>' Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approv.Qty" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvappQty" runat="server" BackColor="White" BorderStyle="None" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.000;(#,##0.000); ") %>' Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnResFooterDelete" runat="server" Font-Bold="True" OnClick="lbtnResFooterDelete_Click" Style="text-align: center;" Width="60px" CssClass="btn btn-primary primaryBtn">Delete</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase Supply Date" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvpursupDat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent" Text='<%# DataBinder.Eval(Container.DataItem, "pursdate").ToString() %>' Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Rate" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbgdrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.000;-#,##0.000; ") %>' Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Purchase Rate" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgvlpurRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lpurrate")).ToString("#,##0.00;(#,##0.00); ") %>' Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current. Rate" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvResRat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.00;(#,##0.00); ") %>' Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appr. Amount" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTAprAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFooterTAprAmt" runat="server" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" Visible="false" />
                                        <asp:TemplateField HeaderText="Supplier Name" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSuplDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ssirdesc").ToString() %>' Width="125px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlSupname" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Requirement">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtorequire" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.000;-#,##0.000; ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>

                                                <asp:LinkButton ID="lbtnCheecked" runat="server" OnClientClick="return Confirmation();" OnClick="lbtnCheecked_Click" CssClass="btn  btn-primary primarygrdBtn">Checked</asp:LinkButton>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Received">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtoreceived" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "treceived")).ToString("#,##0.000;-#,##0.000; ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>


                                            <FooterTemplate>


                                                <asp:LinkButton ID="lbtnFirstApproval" runat="server" OnClientClick="return Confirmation();" OnClick="lbtnFirstApproval_Click" CssClass="btn  btn-primary primarygrdBtn">Approval</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbbgdamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bbgdamt")).ToString("#,##0.000;-#,##0.000; ") %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Master Budget</br> Qty ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtbdqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbgdqty")).ToString("#,##0.000;-#,##0.000; ") %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Master Budget</br> Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtbgqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbbgdqty")).ToString("#,##0.000;-#,##0.000; ") %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvReqNote" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent" Text='<%# DataBinder.Eval(Container.DataItem, "reqnote").ToString() %>' Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ResCode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrsircode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsircode").ToString() %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SupplierCode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsupliercode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ssircode").ToString() %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock Qty" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvStokQty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pstkqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="65px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Supplier Rate" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqsRat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqsrat")).ToString("#,##0.00;(#,##0.00); ") %>' Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="Chk Qty" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvchqty" runat="server" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqty")).ToString("#,##0.000;-#,##0.000; ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>



                            <div class="clearfix"></div>
                            <fieldset class="scheduler-border fieldset_D">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtReqNarr" runat="server" class="form-control" TextMode="MultiLine" Height="60px"></asp:TextBox>
                                            </div>

                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblCCDNarr" runat="server" CssClass="lblTxt" Text="CCD Narr :" Visible="false"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtCCDNarr" runat="server" class="form-control" TextMode="MultiLine" Height="60px" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="col-md-4 pading5px" id="Dept" runat="server">
                                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Department"></asp:Label>
                                            <div class="ddlListPart">
                                                <asp:DropDownList ID="ddlDeptCode" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="3" Style="width: 313px;"></asp:DropDownList>
                                            </div>


                                        </div>
                                        <div class="col-md-4 pading5px" id="uPrj" runat="server" visible="false">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Use Project"></asp:Label>
                                            <div class="ddlListPart">
                                                <asp:DropDownList ID="ddlPrjForUse" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="3" Style="width: 313px;"></asp:DropDownList>
                                            </div>


                                        </div>





                                        <div class="col-md-4 pading5px">
                                            <div class="input-group">
                                                <asp:HyperLink ID="lnkCreateMat" runat="server" CssClass="btn btn-warning primaryBtn" Visible="false"
                                                    NavigateUrl="~/F_17_Acc/AccSubCodeBook.aspx?InputType=Res" Target="_blank">Create Material</asp:HyperLink>


                                            </div>
                                        </div>

                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">

                                                    <asp:Label ID="lblPreparedBy" runat="server" CssClass="lblTxt" Text="Prepared By:" Visible="False"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtPreparedBy" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblApprovedBy" runat="server" CssClass="lblTxt" Text="Approved By:" Visible="False"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtApprovedBy" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblApprovalDate" runat="server" CssClass="lblTxt" Text="Approv.Date:" Visible="False"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtApprovalDate" runat="server" CssClass="form-control inputTxt" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblExpDeliveryDate" runat="server" CssClass="lblTxt" Text="Exp.Del. Date:" Visible="False"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtExpDeliveryDate" runat="server" CssClass="form-control inputTxt" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="clearfix"></div>
                                    </div>

                                </div>

                            </fieldset>

                        </asp:Panel>
                        <asp:Panel ID="PnlDesc" runat="server">
                            <asp:Label ID="lblDescription" runat="server" CssClass="lblTxt panelTitel" Text="Description:"></asp:Label>

                            <div class="table-responsive">
                                <asp:GridView ID="gvDescrip" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered grvContentarea inptNoneBorder">
                                    <PagerSettings Visible="False" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Terms ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTermsID" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "termsid")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvSubject" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "termssubj").ToString() %>' BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvColon" runat="server" Text=" : "></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "termsdesc").ToString() %>' CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "termsrmrk").ToString() %>' CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#F5F5F5" ForeColor="#000" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle />
                                </asp:GridView>
                            </div>
                            <div class="row">
                                <asp:Label ID="lblMurketSurvey" runat="server" CssClass="smLbl_to text-left" Visible="False"></asp:Label>

                                <asp:Label ID="lblsurveyby" CssClass="smLbl_to text-left" runat="server"></asp:Label>
                            </div>
                            <asp:GridView ID="gvMSRInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="274px">
                                <PagerSettings Visible="False" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" Materials Description ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                Width="120px"></asp:Label>
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
                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRSuplDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Concern  Person">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRCperson" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Telephone">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRPhone" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRMobile" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRRate" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brand">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvbrand" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Bold="False" Font-Size="11px"
                                                Height="16px" Style="text-align: left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brand")) %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRDel" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Bold="False" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delivery")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvMSRPayment" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Bold="False" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:Panel>
                    </div>
                </div>
            </div>


            <div class="row">

                <div class="modal " id="detialsinfo" tabindex="-1" role="dialog" aria-labelledby="contactLabel" aria-hidden="true">
                    <div class="modal-dialog modal-md">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" style="text-decoration-color: white">X</button>
                                <h4 class="panel-title" id="contactLabel"><span class="glyphicon glyphicon-hand-right"></span>Add Specification Code</h4>
                            </div>

                            <div class="modal-body">
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <app:label class="control-label">Details:</app:label>
                                        <asp:TextBox ID="txtspcfdesc" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnUpdateSpeDetails" runat="server" class="btn btn-success" aria-hidden="true" OnClientClick="CloseModal();" OnClick="lbtnUpdateSpeDetails_Click">Update</asp:LinkButton>
                                        <button style="float: right;" type="button" class="btn btn-default btn-close" data-dismiss="modal">Close</button>
                                    </div>

                                    <!--<span class="glyphicon glyphicon-remove"></span>-->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

