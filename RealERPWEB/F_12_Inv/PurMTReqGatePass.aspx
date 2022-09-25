<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurMTReqGatePass.aspx.cs" Inherits="RealERPWEB.F_12_Inv.PurMTReqGatePass" EnableEventValidation="false" ValidateRequest="false" %>

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
            var gridview = $('#<%=this.gvAprovInfo.ClientID %>');
            $.keynavigation(gridview);

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
        };
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
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lCurAppdate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                        <asp:TextBox ID="txtCurAprovDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurAprovDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurAprovDate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="lcurGatePassNo" runat="server" CssClass="smLbl_to text-left" Text="Gate Pass No."></asp:Label>
                                        <asp:TextBox ID="lblGatePassNo1" runat="server" CssClass=" smltxtBox" Text="GPN00-"></asp:TextBox>
                                        <asp:TextBox ID="txtGatePassNo2" runat="server" CssClass=" smltxtBox60px" Text="0000"></asp:TextBox>
                                        <asp:Label ID="lblgatepmanualno" runat="server" CssClass="smLbl_to text-left" Text="Gate Pass(Manual)"></asp:Label>
                                        <asp:TextBox ID="txtGatemPassNo" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="2">Ok</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px pull-right">
                                        <asp:Label ID="lblmsg1" runat="server" Visible="false" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblpreGatePassNo" runat="server" CssClass=" smLbl_to" Text="Pre.List"></asp:Label>
                                        <asp:TextBox ID="txtGatePassNo" runat="server" TabIndex="7" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFinGatePass" runat="server" CssClass="btn btn-primary srearchBtn" Style="float: left;" TabIndex="10" OnClick="ImgbtnFinGatePass_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <%-- <asp:ListBox ID="ddlPrevReqList" runat="server" Height="100" CssClass="smDropDown";"></asp:ListBox>--%>

                                        <asp:DropDownList ID="ddlPrevList" runat="server" Style="width: 180px;" CssClass="chzn-select smDropDown inputTxt" TabIndex="11">
                                        </asp:DropDownList>


                                    </div>

                                </div>
                                <panel id="pnlproj" runat="server" visible="false">
                                    <div class="form-group">
                                        <div class="col-md-5">
                                            <asp:Label ID="lblProjectFromList" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                            <asp:DropDownList ID="ddlprjlistfrom" runat="server" Style="width: 380px;" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-5">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">To</asp:Label>
                                            <asp:DropDownList ID="ddlprjlistto" runat="server" CssClass="chzn-select form-control inputTxt" Style="width: 380px;">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:LinkButton ID="lbtnPrject" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnPrject_Click">Show</asp:LinkButton>
                                        </div>

                                    </div>
                                </panel>

                            </div>
                        </fieldset>
                    </div>
                    <div id="Panel0" runat="server">
                        <div class="row">
                        </div>
                    </div>

                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <%--<div class="col-md-3 pading5px asitCol3">
                                                                                         <asp:Label ID="lblResList" runat="server" CssClass="lblTxt lblName" Text="Requisition List"></asp:Label>
                                         <asp:TextBox ID="txtResSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImgbtnFindRes" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindRes_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>--%>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblResList" runat="server" CssClass="smLbl_to" Text="Requisitions"></asp:Label>
                                            <asp:DropDownList ID="ddlResList" runat="server" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" AutoPostBack="true" CssClass="ddlPage chzn-select" Style="width: 280px;" TabIndex="6">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">

                                            <asp:Label ID="lblResList2" runat="server" CssClass=" smLbl_to" Text="Resources"></asp:Label>
                                            <asp:DropDownList ID="ddlResourcelist" runat="server" OnSelectedIndexChanged="ddlResourcelist_SelectedIndexChanged" CssClass="ddlPage chzn-select" Style="width: 280px;" TabIndex="6" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>


                                        <div class="col-md-3">
                                            <asp:Label ID="lblSpecification" runat="server" CssClass="smLbl_to" Text="Specification"></asp:Label>
                                            <asp:DropDownList ID="ddlSpecification" runat="server" CssClass=" ddlPage chzn-select" Style="width: 280px;" TabIndex="6">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:LinkButton ID="lbtnSelectRes" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectRes_Click" TabIndex="2">Select</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectAll_Click" TabIndex="3">Select All</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="gvAprovInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="16px" OnRowDeleting="gvAprovInfo_RowDeleting">
                                    <PagerSettings Visible="False" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Reqno" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer From">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtfpactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer To">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvttpactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Description of Materials">




                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))   %>'
                                                    Width="100px">
                                                </asp:Label>



                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspecification" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlspecification" runat="server" Width="120px">
                                                </asp:DropDownList>
                                            </EditItemTemplate>

                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno1")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnResFooterTotal" runat="server" CssClass="btn btn-primary   primarygrdBtn"
                                                    OnClick="lbtnResFooterTotal_Click">Total :</asp:LinkButton>

                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmrfno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Actual Stock <br>Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvacstockqty" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockbal")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                              

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Req. Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmtrfqty" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdatePurAprov" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdatePurAprov_Click">Final Update</asp:LinkButton>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Bal. Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbalqty" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Approved Qty.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvaprovedQty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "getpqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved </br> " Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvaprovRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:CommandField ShowDeleteButton="True" />

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                <div class="table-responsive">
                                    <asp:Panel ID="Panel3" runat="server">
                                        <fieldset class="scheduler-border fieldset_Nar">
                                            <div class="form-horizontal">

                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol3 ">
                                                    </div>
                                                    <div class="col-md-4 pading5px">
                                                    </div>

                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-6 pading5px">
                                                        <div class="input-group">
                                                            <span class="input-group-addon glypingraddon">
                                                                <asp:Label ID="lblReqNarr0" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                            </span>
                                                            <asp:TextBox ID="txtgetpNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2 pading5px asitCol2">
                                                    </div>
                                                    <div class="col-md-2 pading5px asitCol2">
                                                    </div>
                                                    <div class="col-md-2 pading5px asitCol2">
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <asp:Panel ID="pnlMarketSurvey" runat="server" Visible="false">

                                            <fieldset class="scheduler-border">
                                                <div class="form-horizontal">

                                                    <div class="form-group">
                                                        <div class="col-md-6 pading5px ">
                                                        </div>

                                                    </div>


                                                </div>
                                            </fieldset>


                                        </asp:Panel>
                                    </asp:Panel>
                                </div>
                    </asp:Panel>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
