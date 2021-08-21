<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurReqEntry02.aspx.cs" Inherits="RealERPWEB.F_12_Inv.PurReqEntry02" %>

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
        };
    </script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

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
                                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                                            </div>
                                            <asp:Label ID="lblddlProject" runat="server" Visible="False" CssClass="inputlblVal" Width="337"></asp:Label>


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
                                        <%--<div class="col-md-3 pading5px asitCol3 pull-right">
                                            
                                            <asp:LinkButton ID="lbtnSurVey" runat="server" CssClass="btn btn-primary primaryBtn" Visible="False">Survey</asp:LinkButton>
                                        </div>--%>
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
                                            <asp:TextBox ID="txtMRFNo" runat="server" TabIndex="7" CssClass="inputtextbox" Style="width: 131px;"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 pading5px">

                                            <asp:CheckBox ID="chkdupMRF" runat="server" Text="Dup.M.R.F" CssClass="btn btn-primary checkBox" Visible="false"
                                                TabIndex="9" />
                                            <asp:CheckBox ID="chkneBudget" runat="server" Text="Not Exceed Budget" CssClass="btn btn-primary checkBox" Visible="false"
                                                TabIndex="10" />

                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <div class="msgHandSt">
                                                <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-4 pading5px   pull-right">
                                            <asp:Label ID="lblpreReq" runat="server" CssClass=" smLbl_to" Text="Req. List"></asp:Label>
                                            <asp:TextBox ID="txtSrchMrfNo" runat="server" TabIndex="7" CssClass=" inputtextbox"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindReq" runat="server" CssClass="btn btn-primary srearchBtn" Style="float: left;" OnClick="ImgbtnFindReq_Click" TabIndex="10"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            <asp:DropDownList ID="ddlPrevReqList" runat="server" CssClass="smDropDown inputTxt" TabIndex="11">
                                            </asp:DropDownList>


                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblfloor" runat="server" CssClass="lblTxt lblName" Visible="false" Text="Floor"></asp:Label>
                                            <asp:DropDownList ID="ddlFloor" runat="server" CssClass="smDropDown inputTxt" TabIndex="11">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblddlFloor" runat="server" CssClass="lblTxt lblName" Visible="false" Text="Floor"></asp:Label>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>

                        <div class="clearfix"></div>

                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblResList" runat="server" CssClass="lblTxt lblName" Text="Materials List"></asp:Label>
                                            <asp:TextBox ID="txtResSearch" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImgbtnFindRes" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindRes_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>


                                        </div>
                                        <div class="col-md-2 pading5px asitCol2">

                                            <asp:DropDownList ID="ddlResList" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="True" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>

                                            <asp:Label ID="Label2" runat="server" Visible="False" CssClass="dataLblview label txtAlgLeft"></asp:Label>


                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="lblSpecification" runat="server" CssClass="lblTxt lblDate " Text="Specification"></asp:Label>

                                            <asp:TextBox ID="txtSrchSpecification" runat="server" CssClass="inputtextbox" TabIndex="5"></asp:TextBox>

                                            <asp:LinkButton ID="ImgbtnSpecification" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnSpecification_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>



                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <cc1:ListSearchExtender ID="ListSearchExt1" runat="server"
                                                QueryPattern="Contains" TargetControlID="ddlResList">
                                            </cc1:ListSearchExtender>
                                            <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="smDropDown inputTxt" AutoPostBack="True"  TabIndex="3"></asp:DropDownList>

                                            <asp:LinkButton ID="lbtnSelectRes" runat="server" OnClick="lbtnSelectRes_Click" CssClass="btn btn-primary checkBox">Select</asp:LinkButton>


                                        </div>


                                    </div>




                                </div>
                            </fieldset>



                        </asp:Panel>

                        <div class="clearfix"></div>

                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <asp:GridView ID="gvReqInfo" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnRowDeleting="gvReqInfo_RowDeleting"
                                ShowFooter="True" Width="16px" PageSize="15">
                                <PagerSettings Visible="False" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField HeaderText="Res Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResCod" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Materials">
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlPageNo" runat="server" __designer:wfdid="w67"
                                                AutoPostBack="True" Font-Bold="True" Font-Size="14px"
                                                OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                                Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                Width="150px">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResDesc" runat="server"
                                                Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                                Width="150px">
                                                            
                                                            
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Requirement">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtorequire" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.000;-#,##0.000; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtoreceived" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "treceived")).ToString("#,##0.000;-#,##0.000; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBgdBal" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bbgdqty")).ToString("#,##0.000;-#,##0.000; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Present Stock In Store">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstkqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.000;-#,##0.000; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Present Requirement">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvReqQty" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>

                                         <FooterTemplate>
                                                <asp:LinkButton ID="lbtnResFooterTotal" runat="server" Font-Bold="True" onclick="lbtnResFooterTotal_Click"  CssClass="btn btn-primary  primarygrdBtn">Total :</asp:LinkButton>
                                            </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Site Supply Date">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnUpdateResReq" runat="server" Font-Bold="True"
                                                 OnClick="lbtnUpdateResReq_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvUseDat" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "expusedt").ToString() %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Approv.Qty" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvappQty" runat="server" BackColor="White"
                                                BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnResFooterDelete" runat="server" Font-Bold="True"
                                                Font-Size="14px" ForeColor="White" OnClick="lbtnResFooterDelete_Click"
                                                Style="text-align: center; height: 17px;" Width="50px">Delete</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Last Purchase Rate" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgvlpurRate" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lpurrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Purchase Supply Date" Visible="False">

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvpursupDat" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "pursdate").ToString() %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Current. Rate" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvResRat" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Appr. Amount" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTAprAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFooterTAprAmt" runat="server" ForeColor="White"
                                                Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>









                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvReqNote" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "reqnote").ToString() %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Store Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvstorecode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "storecode").ToString() %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ResCode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvrsircode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsircode").ToString() %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Stock Qty" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvStokQty" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pstkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                            <fieldset class="scheduler-border fieldset_D">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtReqNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
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

                            <%--<table style="width: 100%; height: 133px;">
                                <tr>
                                    <td style="height: auto;" colspan="12" valign="top">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style71">
                                        <asp:Label ID="lblReqNarr" runat="server" CssClass="style15" Font-Bold="True"
                                            Font-Size="12px" Height="16px" Style="text-align: right" Text="Narration:"
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style43" colspan="5">
                                        <asp:TextBox ID="txtReqNarr" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Height="55px"
                                            TextMode="MultiLine" Width="415px"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style71">
                                        <asp:Label ID="lblPreparedBy" runat="server" CssClass="style15"
                                            Font-Bold="True" Font-Size="12px" Height="16px" Style="text-align: right"
                                            Text="Prepared By:" Visible="False" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style76">
                                        <asp:TextBox ID="txtPreparedBy" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Visible="False"
                                            Width="90px"></asp:TextBox>
                                    </td>
                                    <td class="style74">&nbsp;</td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblApprovedBy" runat="server" CssClass="style15"
                                            Font-Bold="True" Font-Size="12px" Height="16px" Style="text-align: right"
                                            Text="Approved By:" Visible="False" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style54">
                                        <asp:TextBox ID="txtApprovedBy" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Visible="False"
                                            Width="120px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right" class="style81">
                                        <asp:Label ID="lblApprovalDate" runat="server" CssClass="style15"
                                            Font-Bold="True" Font-Size="12px" Height="16px" Style="text-align: right"
                                            Text="Approv.Date:" Visible="False" Width="65px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtApprovalDate" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)"
                                            Visible="False" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style71">
                                        <asp:Label ID="lblExpDeliveryDate" runat="server" CssClass="style15"
                                            Font-Bold="True" Font-Size="12px" Height="16px" Style="text-align: right"
                                            Text="Exp.Del. Date:" Visible="False" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style76">
                                        <asp:TextBox ID="txtExpDeliveryDate" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)"
                                            Width="90px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td class="style74">&nbsp;</td>
                                    <td style="text-align: right">&nbsp;</td>
                                    <td colspan="5">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                </tr>
                            </table>--%>
                        </asp:Panel>
                          <div class="clearfix"></div>
                        <asp:Panel ID="PnlDesc" runat="server" Visible="False">
                            <asp:Label ID="lblDescription" runat="server" CssClass="lblTxt panelTitel" Text="Description:"></asp:Label>
                            <div class="table-responsive">
                                <asp:GridView ID="gvDescrip" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="16px">
                                    <PagerSettings Visible="False" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
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
                                                <asp:TextBox ID="txtgvSubject" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "termssubj").ToString() %>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvColon" runat="server" Font-Bold="true" Font-Size="16px"
                                                    Text=" : "></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDesc" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "termsdesc").ToString() %>'
                                                    Width="250px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRemarks" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "termsrmrk").ToString() %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>

                        </asp:Panel>
                    </div>
                </div>
            </div>


            <%--<asp:Panel ID="Panel3" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                BorderWidth="1px">

                <table style="width: 900px">
                    <tr>

                        <td class="style78">
                            <asp:Label ID="Label6" runat="server" CssClass="style15" Font-Bold="True"
                                Font-Size="12px" Style="text-align: left" Text="Project Name:"
                                Width="83px"></asp:Label>
                        </td>
                        <td class="style42">
                            <asp:TextBox ID="txtProjectSearch" runat="server" BorderStyle="None"
                                Height="18px" Style="margin-left: 0px" Width="80px"></asp:TextBox>
                        </td>
                        <td class="style34" align="right">
                            <asp:ImageButton ID="ImgbtnFindProjectName" runat="server" Height="19px"
                                ImageUrl="~/Image/find_images.jpg" Width="16px"
                                OnClick="ImgbtnFindProjectName_Click" TabIndex="1" />
                        </td>
                        <td class="style43" colspan="4">
                            <asp:DropDownList ID="ddlProject" runat="server"
                                Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" TabIndex="2">
                            </asp:DropDownList>
                            <asp:Label ID="lblddlProject" runat="server" __designer:wfdid="w4"
                                BackColor="White" Font-Bold="True" Font-Size="14px" ForeColor="Maroon"
                                Style="font-size: 12px; text-align: left" Visible="False" Width="300px"></asp:Label>
                        </td>
                        <td class="style34" align="right" width="85px">
                            <asp:Label ID="lblCurDate" runat="server" Font-Bold="True" Font-Size="12px"
                                Height="16px" Style="text-align: right" Text="Req.Date:"
                                CssClass="style15"></asp:Label>
                        </td>
                        <td width="125px">
                            <asp:TextBox ID="txtCurReqDate" runat="server" BorderStyle="Solid"
                                BorderWidth="1px" Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)"
                                Width="124px" TabIndex="3"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server"
                                Format="dd.MM.yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>
                        </td>
                        <td class="style47">
                            <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="16px"
                                Height="23px" OnClick="lbtnOk_Click"
                                Style="text-align: center; background-color: #99FFCC" Width="52px"
                                TabIndex="4">Ok</asp:LinkButton>
                        </td>
                        <td>&nbsp;</td>
                        <td class="style46">
                            <asp:Label ID="lblLastReqNo4" runat="server" Font-Bold="True" Font-Size="12px"
                                Style="text-align: right" Text="" Width="80px"></asp:Label>
                        </td>
                        <td class="style19">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style78">
                            <asp:Label ID="lblCurNo" runat="server" CssClass="style15" Font-Bold="True"
                                Font-Size="12px" Height="16px" Style="text-align: left" Text=" Req. No.:"
                                Width="83px"></asp:Label>
                        </td>
                        <td class="style42">
                            <asp:TextBox ID="txtReqText" runat="server" BorderStyle="None"
                                Height="18px" Style="margin-left: 0px" Width="80px" TabIndex="5"></asp:TextBox>
                        </td>
                        <td class="style34" align="right">
                            <asp:ImageButton ID="ImgbtnReqse" runat="server" Height="19px"
                                ImageUrl="~/Image/find_images.jpg" Width="16px"
                                OnClick="ImgbtnReqse_Click" TabIndex="6" />
                        </td>
                        <td class="style67">
                            <asp:Label ID="lblCurReqNo1" runat="server" Font-Bold="True" Font-Size="12px"
                                Style="border: 1px solid #000000; padding: 1px 4px; text-align: right; background-color: #FFFFFF;"
                                Text="REQ00-" Width="45px"></asp:Label>
                        </td>
                        <td class="style66">
                            <asp:TextBox ID="txtCurReqNo2" runat="server" BorderStyle="Solid"
                                BorderWidth="1px" Font-Bold="True" Font-Size="12px" ReadOnly="True"
                                Width="45px" TabIndex="7">00000</asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblmrfno" runat="server" CssClass="style15" Font-Bold="True"
                                Font-Size="12px" Style="text-align: right" Text="M.R.F. No.:" Width="70px"></asp:Label>
                        </td>
                        <td class="style65">
                            <asp:TextBox ID="txtMRFNo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Size="12px" Width="105px" TabIndex="8"></asp:TextBox>
                        </td>
                        <td class="style36">
                            <asp:CheckBox ID="chkdupMRF" runat="server" BackColor="#000066"
                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                Font-Size="12px" ForeColor="Yellow" Text="Dup.M.R.F" Width="80px"
                                TabIndex="9" />
                        </td>
                        <td width="125px">
                            <asp:CheckBox ID="chkneBudget" runat="server" BackColor="#000066"
                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                Font-Size="12px" ForeColor="Yellow" Text="Not Exceed Budget" Width="125px"
                                TabIndex="10" />
                        </td>
                        <td>&nbsp;</td>
                        <td class="style46">&nbsp;</td>
                        <td class="style19">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style78">
                            <asp:Label ID="lblpreReq" runat="server" CssClass="style15" Font-Bold="True"
                                Font-Size="12px" Height="16px" Style="text-align: left" Text="Req. List:"
                                Width="83px"></asp:Label>
                        </td>
                        <td class="style42">
                            <asp:TextBox ID="txtSrchMrfNo" runat="server" BorderStyle="None"
                                Font-Bold="True" Font-Size="12px" Width="80px" TabIndex="11"></asp:TextBox>
                        </td>
                        <td class="style34" align="right">
                            <asp:ImageButton ID="ImgbtnFindReq" runat="server" Height="19px"
                                ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindReq_Click"
                                Width="16px" TabIndex="12" />
                        </td>
                        <td class="style43" colspan="4">
                            <asp:DropDownList ID="ddlPrevReqList" runat="server" Font-Size="12px"
                                Width="300px" TabIndex="13">
                            </asp:DropDownList>
                        </td>
                        <td class="style34" colspan="2">
                            <asp:Label ID="lblmsg1" runat="server" __designer:wfdid="w4" BackColor="Red"
                                Font-Bold="True" Font-Size="12px" ForeColor="White" Height="18px"
                                Style="font-size: 12px; text-align: left"></asp:Label>
                            <asp:Label ID="lblprintstk" runat="server"></asp:Label>
                        </td>
                        <td class="style47"></td>
                        <td>
                            <asp:Label ID="lblfloor" runat="server" CssClass="style15" Font-Bold="True"
                                Font-Size="12px" Style="text-align: right" Text="Floor:" Visible="False"
                                Width="80px"></asp:Label>
                        </td>
                        <td class="style46">
                            <asp:DropDownList ID="ddlFloor" runat="server" AutoPostBack="True"
                                Style="text-transform: capitalize; border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                Visible="False" Width="120px" TabIndex="14">
                            </asp:DropDownList>
                            <asp:Label ID="lblddlFloor" runat="server" __designer:wfdid="w4"
                                BackColor="White" Font-Bold="True" Font-Size="14px" ForeColor="Maroon"
                                Style="text-transform: capitalize; font-size: 12px; text-align: left" Visible="False" Width="120px"></asp:Label>
                        </td>
                        <td class="style19"></td>
                    </tr>

                </table>
            </asp:Panel>--%>


            <table style="width: 100%;">
                <tr>
                    <td class="style18" colspan="13">
                        <%--<asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                            BorderWidth="1px">
                            <table style="width: 100%; height: 10px;">
                                <tr>
                                    <td class="style71">
                                        <asp:Label ID="lblResList" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: left; color: #FFFFFF;" Text="Materials List:"
                                            Width="85px"></asp:Label>
                                    </td>
                                    <td class="style76">
                                        <asp:TextBox ID="txtResSearch" runat="server" BorderStyle="None"
                                            Font-Bold="True" Font-Size="12px" Width="80px" TabIndex="15"></asp:TextBox>
                                    </td>
                                    <td class="style77">
                                        <asp:ImageButton ID="ImgbtnFindRes" runat="server" Height="19px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindRes_Click"
                                            Width="16px" BorderStyle="None" TabIndex="16" />
                                    </td>
                                    <td colspan="4">
                                        <asp:DropDownList ID="ddlResList" runat="server" AutoPostBack="True"
                                            Font-Size="12px" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged"
                                            Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                            Width="385px" TabIndex="17">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="80px">
                                        <asp:Label ID="lblSpecification" runat="server" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: left; color: #FFFFFF;"
                                            Text="Specification:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSrchSpecification" runat="server" BorderStyle="None"
                                            Font-Bold="True" Font-Size="12px" TabIndex="15" Width="80px"></asp:TextBox>
                                    </td>

                                    <td>
                                        <asp:ImageButton ID="ImgbtnSpecification" runat="server" BorderStyle="None"
                                            Height="19px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ImgbtnSpecification_Click" TabIndex="16" Width="16px" />
                                    </td>
                                    <td class="style53">
                                        <cc1:ListSearchExtender ID="ListSearchExt1" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlResList">
                                        </cc1:ListSearchExtender>
                                        <asp:DropDownList ID="ddlResSpcf" runat="server" Font-Size="12px"
                                            Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                            TabIndex="19" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style53">
                                        <asp:LinkButton ID="lbtnSelectRes" runat="server" CssClass="style15"
                                            Font-Bold="True" Font-Size="12px" OnClick="lbtnSelectRes_Click"
                                            Style="text-align: center;" TabIndex="20" Width="54px">Select</asp:LinkButton>
                                    </td>
                                    <td class="style79">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                        
                    </td>
                    <td class="style19">&nbsp;</td>
                </tr>
                <tr>
                    <td class="style18" colspan="14">
                        <%--<asp:Panel ID="PnlDesc" runat="server" BorderColor="Maroon" BorderStyle="Solid"
                            BorderWidth="1px" Visible="False">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style80">
                                        <asp:Label ID="lblDescription" runat="server" CssClass="style15"
                                            Font-Bold="True" Font-Size="12px" Height="16px" Style="text-align: left"
                                            Text="Description:" Width="200px" BackColor="#000066" BorderColor="Yellow"
                                            BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="12">
                                        
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                    </td>
                </tr>
                <tr>
                    <td class="style18">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="style42">&nbsp;</td>
                    <td class="style34">&nbsp;</td>
                    <td class="style55">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="style34">&nbsp;</td>
                    <td class="style39">&nbsp;</td>
                    <td class="style38">&nbsp;</td>
                    <td class="style47">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="style46">&nbsp;</td>
                    <td class="style19">&nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

