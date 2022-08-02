<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurAprovEntry.aspx.cs" Inherits="RealERPWEB.F_14_Pro.PurAprovEntry" %>

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
            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });
        };

        function Confirmation() {
            if (confirm('Are you sure you want to save?')) {
                return;
            } else {
                return false;
            }
        }

        function CloseModal() {
            $('#detialsinfo').modal('hide');

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
                                        <asp:Label ID="lCurAppdate" runat="server" CssClass="lblTxt lblName" Text="Aprov.Date:"></asp:Label>
                                        <asp:TextBox ID="txtCurAprovDate" runat="server" AutoCompleteType="Disabled" CssClass="  inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurAprovDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurAprovDate"></cc1:CalendarExtender>



                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lcurApprNo" runat="server" CssClass="smLbl_to text-left" Text="Aprov. No."></asp:Label>
                                        <asp:Label ID="lblCurAprovNo1" runat="server" CssClass="ddlPage62" Text="PAP00-"></asp:Label>
                                        <asp:TextBox ID="txtCurAprovNo2" runat="server" CssClass="ddlPage" Text="0000"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="2">Ok</asp:LinkButton>
                                        </div>


                                    </div>


                                    <div class="col-md-3 pading5px pull-right">
                                        <asp:Label ID="lblmsg1" runat="server" Visible="false" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                    <div class="col-md-5pading5px asitCol25 pull-right">
                                        <asp:LinkButton ID="lbtnPrevAprovList" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevAprovList_Click"
                                            TabIndex="3">Previous List</asp:LinkButton>

                                        <asp:DropDownList ID="ddlPrevAprovList" runat="server" Width="180px" CssClass="inputTxt inpPixedWidth" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>

                                </div>


                            </div>
                        </fieldset>
                    </div>
                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblResList" runat="server" CssClass="lblTxt lblName" Text="Requisition List"></asp:Label>
                                            <asp:TextBox ID="txtResSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImgbtnFindRes" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindRes_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:DropDownList ID="ddlResList" runat="server" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" CssClass="chzn-select form-control  inputTxt" TabIndex="6" AutoPostBack="True" Width="200px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:Label ID="lblResList2" runat="server" CssClass=" smLbl_to" Text="Resources"></asp:Label>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:DropDownList ID="ddlResourcelist" runat="server" OnSelectedIndexChanged="ddlResourcelist_SelectedIndexChanged" CssClass="form-control inputTxt" TabIndex="6" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="lbtnSelectRes" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectRes_Click" TabIndex="2">Select</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectAll_Click" TabIndex="2">Select All</asp:LinkButton>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <a class="btn btn-primary srearchBtn pull-left" data-toggle="modal" data-target="#detialsinfo" data-original-title><span class="glyphicon glyphicon-plus"></span></a>
                                            <asp:Label ID="lblResList0" runat="server" CssClass="lblName lblTxt smLbl_to" Text="Supp. List"></asp:Label>
                                            <asp:TextBox ID="txtSupSearch" runat="server" Style="margin-left: -20px;" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1 colMdbtn" style="margin-left: -45px;">
                                            <asp:LinkButton ID="ImgbtnFindSup" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSup_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>

                                        <div class="col-md-2 pading5px asitCol3">
                                            <asp:DropDownList ID="ddlSupList" runat="server" CssClass="chzn-select form-control  inputTxt" TabIndex="6">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:Label ID="lblSpecification" runat="server" CssClass="smLbl_to" Text="Specification"></asp:Label>

                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="form-control inputTxt" TabIndex="6">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblResList1" runat="server" CssClass="lblTxt lblName text-left" Text="Payment" Width="50px"></asp:Label>
                                            <asp:DropDownList ID="ddlPayType" runat="server" CssClass="inpPixedWidth inputTxt" Style="padding: 0 2px;"
                                                TabIndex="13">
                                                <asp:ListItem>Credit</asp:ListItem>
                                                <asp:ListItem>Cash</asp:ListItem>
                                                <asp:ListItem>A/C Payee</asp:ListItem>
                                                <asp:ListItem>PDC</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>


                                    </div>
                                    <div class="form-group">
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="gvAprovInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="16px" OnRowCancelingEdit="gvAprovInfo_RowCancelingEdit"
                                    OnRowEditing="gvAprovInfo_RowEditing"
                                    OnRowUpdating="gvAprovInfo_RowUpdating" OnRowDeleting="gvAprovInfo_RowDeleting">
                                    <PagerSettings Visible="False" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--Item Serial RowID add for Manama--%>
                                         <asp:TemplateField HeaderText="Item Sl" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItemSl" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"rowid")) %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPrjCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reqno" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supl Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResCod1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProjDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projdesc1")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:CheckBox ID="ChkgvFooterHide" runat="server" AutoPostBack="True" Font-Size="11px"
                                                    ForeColor="Black" OnCheckedChanged="ChkgvFooterHide_CheckedChanged" Style="font-weight: 400"
                                                    Text="Hide 0 qty" Width="70px" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlPageNo" runat="server" __designer:wfdid="w67" AutoPostBack="True"
                                                    Font-Bold="True" Font-Size="14px" OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                                    Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                    Width="100px">
                                                </asp:DropDownList>
                                            </FooterTemplate>




                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1"))   %>'
                                                    Width="100px">
                                                </asp:Label>



                                            </ItemTemplate>
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
                                                <asp:Label ID="lblgvReqNo1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnResFooterTotal" runat="server" CssClass="btn btn-primary   primarygrdBtn"
                                                    OnClick="lbtnResFooterTotal_Click">Total :</asp:LinkButton>

                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmrfno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprQty" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdatePurAprov" runat="server" CssClass="btn btn-danger primaryBtn" OnClientClick="return Confirmation();" OnClick="lbtnUpdatePurAprov_Click">Final Update</asp:LinkButton>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Program Completed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvComqty" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance Qty">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-danger primaryBtn" OnClientClick="return Confirmation();"
                                                    OnClick="lbtnDelete_Click">Delete</asp:LinkButton>

                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBalqty" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New Order </br> Qty.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvNewOrderQty" runat="server" BorderColor="#69AEE7" BackColor="" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" BackColor="#69AEE7" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved </br> Rate(Mgt.)" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmgtaprovRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maprovrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvApprovsRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovsrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Discount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvdispercnt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dispercnt")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dispercnt"))>0?"%":"")  %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Actual Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvNewApprovRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New Order Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNewOrderAmt" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFooterTAprovAmt" runat="server" Width="60px" ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:CommandField CancelText="Can" ShowEditButton="True" UpdateText="Up" />

                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSuplDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ssirdesc1").ToString() %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlSupname" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </EditItemTemplate>

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



                                        <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payment Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayType" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEntryUser" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eusrname")) %>'
                                                    Width="50px"></asp:Label>
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
                                                        <asp:Label ID="lblApprovedBy" runat="server" CssClass="lblTxt lblName" Text="Approved By:" Visible="False"></asp:Label>
                                                        <asp:Label ID="lblApprovalDate" runat="server" CssClass="lblTxt lblName" Text="Approv.Date" Visible="False"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4 pading5px">

                                                        <asp:Label ID="lblPreparedBy" runat="server" CssClass="lblTxt lblName" Text="Prepared By" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>

                                                    </div>

                                                </div>


                                                <div class="form-group">
                                               <div class="col-md-12  pading5px">
                                               <asp:Label ID="lblreqnaration" runat="server" class="lblTxt lblName" Width="900px" Text="Req Narration: "  Font-Bold="true"   style="text-align:left"> </asp:Label>

                                               </div>


                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-6 pading5px">
                                                        <div class="input-group">
                                                            <span class="input-group-addon glypingraddon">                
                                                                <asp:Label ID="lblReqNarr0" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                            </span>
                                                            <asp:TextBox ID="txtAprovNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <asp:LinkButton ID="lbtnAprove" runat="server" OnClick="lbtnAprove_Click" Visible="false" CssClass="btn btn-primary primaryBtn"
                                                        TabIndex="20">Approved</asp:LinkButton>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2 pading5px asitCol2">
                                                        <asp:TextBox ID="txtApprovedBy" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 pading5px asitCol2">
                                                        <asp:TextBox ID="txtApprovalDate" runat="server" class="form-control" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2 pading5px asitCol2">
                                                        <asp:TextBox ID="txtPreparedBy" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <asp:Panel ID="pnlMarketSurvey" runat="server" Visible="false">

                                            <fieldset class="scheduler-border">
                                                <div class="form-horizontal">

                                                    <div class="form-group">
                                                        <div class="col-md-6 pading5px ">
                                                            <asp:Label ID="lblMurketSurvey" runat="server" CssClass="lblTxt lblName" Text="Market Survey"></asp:Label>
                                                            <asp:Label ID="lblsurveyby" runat="server" CssClass="lblTxt lblName" Text="Approv.Date" Visible="False"></asp:Label>
                                                        </div>

                                                    </div>


                                                </div>
                                            </fieldset>

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
                                                    <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvMSRResCod" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvMSRSpcfCod" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvMSRSuplCod" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
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
                                                                Style="text-align: left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delivery")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvMSRPayment" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px" Font-Bold="False" Font-Size="11px"
                                                                Style="text-align: left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payment")) %>'
                                                                Width="100px"></asp:TextBox>
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
                                    </asp:Panel>
                                </div>

                                <div class="modal " id="detialsinfo" tabindex="-1" role="dialog" aria-labelledby="contactLabel" aria-hidden="true">
                                    <div class="modal-dialog modal-md">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                                <h4 class="panel-title" id="contactLabel"><span class="glyphicon glyphicon-info-sign"></span>Add Supplier</h4>
                                            </div>

                                            <div class="modal-body">
                                                <div class="row">
                                                    <label class="control-label">Details:</label>

                                                    <asp:TextBox ID="txtspcfdesc" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>



                                                </div>
                                                <div class="panel-footer">


                                                    <asp:LinkButton ID="lbtnUpdateSpeDetails" runat="server" class="btn btn-success" aria-hidden="true" OnClientClick="CloseModal();" OnClick="lbtnUpdateSpeDetails_OnClick">Update</asp:LinkButton>
                                                    <button style="float: right;" type="button" class="btn btn-default btn-close" data-dismiss="modal">Close</button>

                                                    <!--<span class="glyphicon glyphicon-remove"></span>-->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                    </asp:Panel>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

