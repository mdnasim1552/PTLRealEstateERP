<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurMRREntry.aspx.cs" Inherits="RealERPWEB.F_12_Inv.PurMRREntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


            var gridview = $('#<%=this.gvMRRInfo.ClientID %>');
            $.keynavigation(gridview);
            $('.chzn-select').chosen({ search_contains: true });



            $('[id*=listGroup]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });



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
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName" Text="MRR Date"></asp:Label>
                                        <asp:TextBox ID="txtCurMRRDate" runat="server" AutoPostBack="True" CssClass="inputTxt inputDateBox" TabIndex="1" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurMRRDate_CalendarExtender" runat="server"
                                            Format="dd.MM.yyyy" TargetControlID="txtCurMRRDate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4 ">
                                        <asp:Label ID="Label10" runat="server" CssClass="smLbl_to" Text="Reference No."></asp:Label>
                                        <asp:Label ID="lblCurMRRNo1" runat="server" CssClass=" smltxtBox " Text="MRR00-" Width="45px"></asp:Label>
                                        <asp:TextBox ID="txtCurMRRNo2" runat="server" CssClass=" smltxtBox60px disabled" ReadOnly="True">00000</asp:TextBox>
                                        <asp:Label ID="Label9" runat="server" CssClass="smLbl_to">MRR No.</asp:Label>

                                        <asp:TextBox ID="txtMRRRef" runat="server" TabIndex="3" CssClass=" inputtextbox" Style="width: 78px"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">

                                        <asp:Label ID="lblmsg1" CssClass=" btn btn-danger primaryBtn" runat="server" Visible="false"></asp:Label>



                                    </div>
                                    <div class="col-md-2 pading5px asitCol2 pull-right">
                                        <asp:LinkButton ID="ImgbtnPreMRR" runat="server" Style="margin-left: -118px;" CssClass="lblTxt lblName" OnClick="ImgbtnPreMRR_Click"
                                            TabIndex="3">Previous MRR</asp:LinkButton>

                                        <asp:DropDownList ID="ddlPrevMRRList" runat="server" Style="width: 200px;" CssClass=" chzn-select inputTxt inpPixedWidth" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProjectList0" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Project List"></asp:Label>
                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass=" inputtextbox" TabIndex="4"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProject_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" CssClass="chzn-select form-control  inputTxt"
                                            OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" TabIndex="6">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblddlProject" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:CheckBox ID="chkdupMRR" runat="server" Text="Dup.MRR" CssClass="btn btn-primary checkBox" />
                                    </div>

                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblsuplist" runat="server" CssClass="lblTxt lblName " Text="Supplier"></asp:Label>
                                        <asp:TextBox ID="txtSupSearch" runat="server" CssClass=" inputtextbox" TabIndex="4"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImgbtnFindSup" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSup_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSupList" runat="server" AutoPostBack="True"
                                            Font-Size="12px" OnSelectedIndexChanged="ddlSupList_SelectedIndexChanged"
                                            TabIndex="9" CssClass="chzn-select form-control  inputTxt">
                                        </asp:DropDownList>


                                    </div>

                                    <div class="col-md-5">
                                        <div class="col-md-8">
                                            <asp:Label ID="lblOrderList" runat="server" CssClass=" smLbl_to " Text="Order"></asp:Label>
                                            <asp:DropDownList ID="ddlOrderList" runat="server"
                                                TabIndex="9" CssClass="chzn-select ddlPage" Width="250px">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-4">

                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click" TabIndex="11">Ok</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <asp:Panel ID="Panel1" runat="server" Visible="False">

                            <fieldset class="scheduler-border fieldset_B">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblchllno" runat="server" CssClass="lblTxt lblName" Text="Chalan No"></asp:Label>
                                            <asp:TextBox ID="txtChalanNo" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        </div>

                                        <div class="col-md-9 pading5px ">
                                            <asp:Label ID="lblqtycertificate" runat="server" CssClass="lblTxt lblName" Text="QC No"></asp:Label>
                                            <asp:TextBox ID="txtQc" runat="server" Width="120px" CssClass="inputTxt inputDateBox"></asp:TextBox>


                                            <asp:Label ID="lblChaDate" runat="server" CssClass=" lblTxt lblName" Width="120px"></asp:Label>
                                            <asp:TextBox ID="txtChaDate" runat="server" AutoPostBack="True" CssClass=" inputtextbox" TabIndex="1" ToolTip="(dd.mm.yyyy)" Style="width: 78px;"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd.MM.yyyy" TargetControlID="txtChaDate"></cc1:CalendarExtender>
                                        </div>



                                    </div>


                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblResList" runat="server" CssClass="lblTxt lblName" Text="Resource List"></asp:Label>
                                            <asp:TextBox ID="txtResSearch" runat="server" CssClass=" inputtextbox" TabIndex="4"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImgbtnFindRes" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindRes_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>

                                        </div>
                                        <%--   <div class="col-md-4 pading5px asitCol4">--%>
                                        <%--<asp:DropDownList ID="ddlResList" runat="server" CssClass="form-control inputTxt" TabIndex="6"></asp:DropDownList>--%>
                                        <%--  </div>--%>

                                        <%--<cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" CssClass=""
                                                MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True" Width="350px">
                                            </cc1:DropCheck>--%>
                                        <%-- <div class="col-md-3 pading5px asitCol3">
                                          
                                      </div>--%>
                                        <div class="col-md-4 pading5px">
                                            <asp:ListBox ID="listGroup" runat="server" CssClass="form-control" Style="min-width: 360px !important;" SelectionMode="Multiple"></asp:ListBox>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnSelectRes_Click" TabIndex="11" Style="margin-left: 15px; float: left !important;">Select</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnSelectResAll" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnSelectResAll_Click" TabIndex="11" Visible="false">Select All</asp:LinkButton>
                                            <%--<asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnSelectRes_Click"  TabIndex="11" style="margin:-20px 0 0 350px; float:left !important;">Select</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnSelectResAll" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnSelectResAll_Click" TabIndex="11" style="margin:-20px 0 0 20px; float:left !important;" Visible="false">Select All</asp:LinkButton>--%>
                                        </div>


                                        <asp:Label ID="Label2" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>

                                    </div>


                                </div>
                            </fieldset>

                        </asp:Panel>

                        <asp:GridView ID="gvMRRInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            ShowFooter="True" OnRowDeleting="gvMRRInfo_RowDeleting" CssClass="table-striped table-hover table-bordered grvContentarea" OnSelectedIndexChanged="gvMRRInfo_SelectedIndexChanged">

                            <PagerSettings Visible="False" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--Item Serial RowID add for Manama--%>
                                <asp:TemplateField HeaderText="Item Sl" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemSl" runat="server" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"rowid")) %>' Width="15px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Req No." Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvReqnomain" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Res Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />



                                <asp:TemplateField HeaderText="Req No.">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnResFooterTotal" runat="server"
                                            OnClick="lbtnResFooterTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total :</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvReqno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Materials">
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlPageNo" runat="server" __designer:wfdid="w67" AutoPostBack="True"
                                            Font-Bold="True" Font-Size="14px" OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
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
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecuptodate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recup")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Qty.">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnDelMRR" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnDelMRR_Click">Delete</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderBal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderbal")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="This MRR">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdateMRR" runat="server" OnClientClick="return Confirmation()" OnClick="lbtnUpdateMRR_Click" CssClass="btn btn-danger primarygrdBtn">Update</asp:LinkButton>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvMRRQty" runat="server" BackColor="White" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" BackColor="#69AEE7" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMRRRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMRRAmt" runat="server" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFooterTMRRAmt" runat="server" Width="80px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chalan Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvChlnqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chlnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvMRRNote" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "mrrnote").ToString() %>' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                        <asp:Panel ID="PnlNarration" runat="server" Visible="false">

                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblreqnaration" runat="server" class="lblTxt lblName" Width="900px" Text="Req Narration: " Font-Bold="true" Style="text-align: left"> </asp:Label>

                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-8 pading5px inputtxtNarration">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt lblName" Text="Narration"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtMRRNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>



                                    </div>

                                </div>
                            </fieldset>
                        </asp:Panel>
                        <table>
                            <tr>
                                <td class="style15">
                                    <asp:Label ID="lblPreparedBy" runat="server" Font-Bold="True" Font-Size="12px"
                                        Height="16px" Style="text-align: right" Text="Prepared By:" Visible="False"
                                        Width="99px"></asp:Label>
                                </td>
                                <td class="style20">
                                    <asp:TextBox ID="txtPreparedBy" runat="server" BorderStyle="Solid"
                                        BorderWidth="1px" Font-Bold="True" Font-Size="12px" Visible="False"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblApprovedBy" runat="server" Font-Bold="True" Font-Size="12px"
                                        Height="16px" Style="text-align: right" Text="Approved By:" Visible="False"
                                        Width="80px"></asp:Label>
                                </td>
                                <td class="style71">
                                    <asp:TextBox ID="txtApprovedBy" runat="server" BorderStyle="Solid"
                                        BorderWidth="1px" Font-Bold="True" Font-Size="12px" Visible="False"
                                        Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblApprovalDate" runat="server" Font-Bold="True"
                                        Font-Size="12px" Height="16px" Style="text-align: right" Text="Approv.Date:"
                                        Visible="False" Width="66px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtApprovalDate" runat="server" BorderStyle="Solid"
                                        BorderWidth="1px" Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)"
                                        Visible="False" Width="100px"></asp:TextBox>
                                </td>
                                <td class="style69">&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                                <td class="style60">&nbsp;
                                </td>
                                <td class="style53">&nbsp;
                                </td>
                            </tr>
                        </table>


                    </div>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
