<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurConWrkOrderEntry.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurConWrkOrderEntry" %>

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


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>
    <style type="text/css">
        .lineheight {
        }

        .customchk {
            padding: 2px;
            margin-left: 20px;
        }

            .customchk label {
                padding-left: 5px;
            }

        .moduleItemWrpper .btn-info {
            background-color: #5bc0de;
            color: #000;
            font-weight: bold;
            border: 1px solid #155273 !important;
            display: inline-table !important;
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblCurNo" runat="server" CssClass="lblTxt lblName" Text="Order No:"></asp:Label>
                                        <asp:Label ID="lblCurISSNo1" runat="server" CssClass="inputTxt inputBox50px"></asp:Label>
                                        <asp:TextBox ID="txtCurISSNo2" runat="server" CssClass="inputTxt inputBox50px" TabIndex="3">000</asp:TextBox>

                                        <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>


                                        <asp:Label ID="lblorderref" runat="server" CssClass="smLbl_to" Text="SMCR.No."></asp:Label>

                                        <asp:TextBox ID="txtOrderRef" runat="server" CssClass=" inputtextbox" TabIndex="3" Style="width: 195px;"></asp:TextBox>
                                        <div class="col-md-3 pading5px pull-right">
                                            <asp:LinkButton ID="lbtnPrevList" runat="server" CssClass="lblTxt lblName prvLinkBtn" OnClick="lbtnPrevList_Click">Prev. List:</asp:LinkButton>
                                            <asp:DropDownList ID="ddlPrevList" runat="server" CssClass=" ddlPage inputTxt" Width="130px" AutoPostBack="True" TabIndex="3"></asp:DropDownList>

                                        </div>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblContractorList" runat="server" CssClass="lblTxt lblName" Text="Contractor Name"></asp:Label>
                                        <asp:TextBox ID="txtsrchContractor" runat="server" CssClass=" inputtextbox" TabIndex="10"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnFindContractor" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="lbtnFindContractor_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-md-4 pading5px">

                                        <asp:DropDownList ID="ddlContractorlist" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="True" TabIndex="3" Style="width: 385px;"></asp:DropDownList>
                                        <asp:Label ID="lblddlContractor" runat="server" Visible="False" Style="width: 380px;" CssClass=" form-control   inputtextbox"></asp:Label>

                                    </div>

                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="11">Ok</asp:LinkButton>

                                    </div>

                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg1" CssClass="btn btn-danger primaryBtn" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProjectList" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtsrchproject" runat="server" CssClass=" inputtextbox" TabIndex="10"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="lbtnFindProject_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="True" TabIndex="3" Style="width: 385px;"></asp:DropDownList>
                                        <asp:Label ID="lblddlProject" runat="server" Visible="False" Style="width: 380px;" CssClass=" form-control   inputtextbox"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="PnlRes" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_B">

                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblfloorno" runat="server" CssClass="lblTxt lblName" Text="Floor No"></asp:Label>
                                            <asp:DropDownList ID="ddlfloorno" runat="server" CssClass=" form-control inputTxt" Width="96" OnSelectedIndexChanged="ddlfloorno_SelectedIndexChanged" TabIndex="12" AutoPostBack="True">
                                                <asp:ListItem Selected="True" Text="Unspecified" Value="00"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-3 pading5px asitCol3" style="width: 155px;">
                                            <asp:Label ID="lblLabour" runat="server" CssClass=" smLbl_to" Text="Labour"></asp:Label>
                                            <asp:TextBox ID="txtSearchLabour" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSearchMaterisl_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" CssClass=" "
                                            MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True" Width="350px">
                                        </cc1:DropCheck>

                                        <%-- <asp:DropDownList ID="ddllabour" runat="server" CssClass=" form-control inputTxt" TabIndex="16" >
                                                </asp:DropDownList>--%>

                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lbtnSelect_Click" CssClass="btn btn-primary primaryBtn"
                                                TabIndex="17">Select</asp:LinkButton>

                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt smLbl_to" Text="Page"></asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Style="margin-left: 6px;" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>


                        </asp:Panel>
                        <div class="table-responsive">
                            <asp:GridView ID="grvissue" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" ShowFooter="True" Width="599px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                OnRowDeleting="grvissue_RowDeleting" OnPageIndexChanging="grvissue_PageIndexChanging" PageSize="15">
                                <PagerSettings Position="TopAndBottom" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField HeaderText="Item Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Floor Desc.">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary primaryBtn"
                                                OnClick="lnkTotal_Click">Total</asp:LinkButton>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblFloordes" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Spec">

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtspec" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spec")) %>' Width="100px" BorderStyle="None" TextMode="MultiLine"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Description">

                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger primaryBtn"
                                                OnClick="lnkupdate_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblwrkdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="200px"></asp:Label>
                                            <asp:TextBox ID="txtwrkdesc" BackColor="Transparent" BorderStyle="None" runat="server" TextMode="MultiLine" Height="50px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sdetails")).Trim() %>'
                                                Width="500px">
                                            <%--sdetails--%>
                                            </asp:TextBox>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="Label14" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lblgvFQty" runat="server" Style="text-align: right"
                                                Width="70px" Font-Size="12px" ForeColor="#000"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvrate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvAmount" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px" BackColor="Transparent" Style="text-align: right" BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lblgvFamount" runat="server" Style="text-align: right"
                                                Width="80px" Font-Size="12px" ForeColor="#000"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtisurmk" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                Width="150px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                            </asp:GridView>
                        </div>

                    </div>

                    <div class="row">
                        <asp:Panel ID="PnlNarration" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-12 pading5px ">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt" Text="Date Of Commencement Of Work:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtcomncdat" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtcomncdat"></cc1:CalendarExtender>
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="Label2" runat="server" CssClass="lblTxt" Text="Date Of Completion:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtcompltdat" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtcompltdat"></cc1:CalendarExtender>

                                                <asp:CheckBox ID="ChkLanguage" runat="server" AutoPostBack="True" Text="Terms & Conditions Bangla" CssClass="btn btn-info customchk"
                                                    Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12 pading5px ">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Subject:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtSubject" runat="server" class="form-control inputTxt">Work Order For Materials</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Dear Vendor,"></asp:Label>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text=":"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtLETDES" runat="server" class="form-control inputTxt"> </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2 pading5px">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt lblName" Text="Term & Condition:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtTerm" runat="server" class="form-control" Rows="2" TextMode="MultiLine" Style="height: 300px; line-height: 18px;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px">
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

