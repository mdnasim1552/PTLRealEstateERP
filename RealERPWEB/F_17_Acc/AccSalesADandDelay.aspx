
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccSalesADandDelay.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccSalesADandDelay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {


            var gridview = $('#<%=this.dgv2.ClientID %>');
            $.keynavigation(gridview);
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

                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName" Text="Voucher No"></asp:Label>
                                        <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True" TabIndex="4"></asp:TextBox>
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ReadOnly="True" TabIndex="5"></asp:TextBox>


                                        <%--<asp:ImageButton ID="ibtnvounu" runat="server" Height="16px" ImageUrl="~/Image/movie_26.gif"
                                            OnClick="ibtnvounu_Click" Width="145px" Visible="False" />--%>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inputDateBox" TabIndex="3"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn okBtn" OnClick="lbtnOk_Click" TabIndex="8">Ok</asp:LinkButton>


                                    </div>

                                    <div class="col-md-3 pull-right">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <%--<div class="form-group">

                                    

                                    <div class="clearfix"></div>


                                </div>--%>
                            </div>
                    </div>
                    <asp:Panel ID="pnlBill" runat="server" Visible="False">


                        <div class="form-group">
                            <div class="col-md-1 pading5px">
                                <asp:CheckBox ID="chkAdustCOde" runat="server" TabIndex="10" Text="Adjusted" CssClass="btn btn-primary checkBox" AutoPostBack="True" OnCheckedChanged="Adjusted_CheckedChanged" />
                            </div>
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblADAndDelay" runat="server" CssClass="  lblTxt lblName" Text="ADW List"></asp:Label>
                                <asp:TextBox ID="txtMRno" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>
                                <div class="colMdbtn">
                                    <asp:LinkButton ID="imgSearchMrno" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgSearchMrno_Click" TabIndex="1">
                                        <span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-md-5 pading5px asitCol5">
                                <asp:DropDownList ID="ddlMRList" runat="server" CssClass="form-control inputTxt" TabIndex="2">
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-1">
                                <asp:LinkButton ID="lbtnSelectMR" runat="server" CssClass="btn btn-primary primaryBtn"
                                    OnClick="lbtnSelectMR_Click"
                                    TabIndex="8">Select</asp:LinkButton>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                    </asp:Panel>


                    <asp:Panel ID="pnlAcHead" runat="server" Visible="False">

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblAccHead" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Accounts Head"></asp:Label>
                                        <asp:TextBox ID="txtSerchAccHead" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnSrchAcchead" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrchAcchead_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-4 pading5px  asitCol4">
                                        <asp:DropDownList ID="ddlAccHead" runat="server" CssClass="form-control chzn-select" TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="ddlAccHead_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </div>



                                </div>

                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblreshead" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Resource Head"></asp:Label>
                                        <asp:TextBox ID="txtsrchres" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnreshead" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="lbtnreshead_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-4 pading5px  asitCol4">
                                        <asp:DropDownList ID="ddlresource" runat="server" CssClass="form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>



                                </div>
                            </div>
                        </fieldset>



                    </asp:Panel>

                    <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Style="text-align: left" Width="685px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccCod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpclCod" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="A/C Description" ItemStyle-Font-Size="9px">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccdesc1" runat="server" Font-Size="11px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")   
                                                                        %>'
                                        Width="350px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Font-Size="11px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFDrAmt" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                        Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDrAmt" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="11px" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#000"
                                    HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCrAmt" runat="server" BackColor="Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="11px" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#000"
                                    HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="AdNo" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbladno" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                    <asp:Panel ID="Panel1" runat="server" Visible="True">

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class=" form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblRefNum" runat="server" CssClass="smLbl_to" Text="Ref./Cheq No/Slip No."></asp:Label>
                                        <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputTxt inpPixedWidth" AutoCompleteType="Disabled" TabIndex="4"></asp:TextBox>

                                        <asp:Label ID="lblSrInfo" runat="server" CssClass="smLbl_to" Text="Narration"></asp:Label>

                                        <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputTxt inpPixedWidth" AutoCompleteType="Disabled" TabIndex="4"></asp:TextBox>

                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="lblNaration" runat="server" CssClass="lblName lblTxt" Text="Narration"></asp:Label>


                                    </div>

                                    <div class="col-md-7 pading5px">
                                        <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled"
                                            CssClass="form-control" Rows="5" Height="150" TextMode="MultiLine"
                                            TabIndex="21"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn"
                                            OnClick="lnkFinalUpdate_Click"
                                            TabIndex="22">Final Update</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

