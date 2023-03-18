<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccDamageAndLost.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccDamageAndLost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {


         <%--  var gridview = $('#<%=this.dgv2.ClientID %>');
           $.keynavigation(gridview);--%>
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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblcurVounum" runat="server" CssClass="smLbl_to" Text="Voucher No."></asp:Label>
                            <div class="d-flex">
                                <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="form-control form-control-sm" Text="JV00-" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="form-control form-control-sm disabled" ReadOnly="True">00000</asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblDate" runat="server" CssClass="control-label" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm" ToolTip="dd-MMM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                    </div>

                    <div class="row" id="pnlTrans" runat="server" visible="false">

                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblBillList" runat="server" CssClass="control-label" Text="Transfer List"></asp:Label>
                                <asp:TextBox ID="txtSrclsdNo" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnLsdNo" runat="server" OnClick="imgbtnLsdNo_Click"><i class="fa fa-search"> </i></asp:LinkButton>
                                <asp:DropDownList ID="ddlTrnsList" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>


                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnSelectTrns" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnSelectTrns_Click">Select</asp:LinkButton>
                            </div>
                        </div>

                    </div>
                    <%-- <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName"> Voucher No.</asp:Label>
                                        <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ReadOnly="True"></asp:TextBox>

                                    </div>
                                    <div class="col-md-4 pading5px">

                                        <asp:Label ID="lblDate" runat="server" CssClass="smLbl" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate">
                                        </cc1:CalendarExtender>

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>





                                    </div>

                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>

                                
                                   

                                




                            </div>
                        </fieldset>
                         <asp:Panel ID="pnlTrans" runat="server" Visible="False">
                        <fieldset class="scheduler-border fieldset_A">

                           
                            <div class="form-horizontal">
                                
                                 <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblBillList" runat="server" CssClass="lblTxt lblName" Text="Transfer List"></asp:Label>
                                            <asp:TextBox ID="txtSrclsdNo" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgbtnLsdNo" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnLsdNo_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>

                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlTrnsList" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">



                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnSelectTrns" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectTrns_Click">Select</asp:LinkButton>

                                            </div>



                                        </div>

                                    </div>
                                </div>
                            </fieldset>

                        </asp:Panel>--%>
                </div>
            </div>
            <div class="card card-fluid" style="min-height: 500px;">
                <div class="card-body">
                    <div class="row">
                        <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False"
                            CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="689px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
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
                                        <asp:Label ID="lblResCod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpclCod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="A/C Description" ItemStyle-Font-Size="9px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc1" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                            Width="350px"></asp:Label>

                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Quantity"
                                    ItemStyle-Font-Size="11px">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvQty" runat="server" ReadOnly="true"
                                            Visible="False" Width="70px"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty" runat="server" ReadOnly="True" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRate" runat="server" ReadOnly="True" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle ForeColor="White" />
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvDrAmt" runat="server" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Width="70px"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvCrAmt" runat="server" ReadOnly="true" BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Width="70px"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" ItemStyle-Font-Size="11px" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRemarks" runat="server" ReadOnly="True"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Billno" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbLsdnno" runat="server" CssClass="GridLebelL" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lsdno")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                        </asp:GridView>

                    </div>
                    <div class="row" id="PnlNarration" runat="server" visible="false">
                        <div class="col-md-1">
                            <asp:Label ID="lblRefNum" runat="server" CssClass="control-label" Text="Ref. No"></asp:Label>
                            <asp:TextBox ID="txtRefNum" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblSrInfo" runat="server" CssClass="control-label" Text="Other ref.(if any)"></asp:Label>
                            <asp:TextBox ID="txtSrinfo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblNaration" runat="server" CssClass="control-label" Text="Narration"></asp:Label>
                            <asp:TextBox ID="txtNarration" runat="server" CssClass="form-control form-control-sm" Rows="2" TextMode="MultiLine"></asp:TextBox>
                            <asp:CheckBox ID="chkpost" runat="server" TabIndex="10" Text="post" CssClass="btn btn-primary checkBox" Visible="false" />
                        </div>




                    </div>
                    <%--<div class="row">
                        <asp:Panel ID="PnlNarration" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3 ">
                                            <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>

                                            <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputtextbox"></asp:TextBox>



                                        </div>
                                        <div class="col-md-4 pading5px">

                                            <asp:Label ID="lblSrInfo" runat="server" CssClass="lblTxt lblName" Text="Other ref.(if any)"></asp:Label>
                                            <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox"></asp:TextBox>




                                        </div>

                                        <div class="col-md-2 pading5px">
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="col-md-6 pading5px">
                                            <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click">Update</asp:LinkButton>
                                            <a class=" btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=17")%>' style="margin: 0 0 0 5px;">Next</a>
                                        </div>

                                        <asp:CheckBox ID="chkpost" runat="server" TabIndex="10" Text="post" CssClass="btn btn-primary checkBox" Visible="false" />


                                    </div>
                                </div>

                            </fieldset>
                        </asp:Panel>

                    </div>--%>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>




