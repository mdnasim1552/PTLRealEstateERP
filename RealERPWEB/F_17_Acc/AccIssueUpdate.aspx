<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccIssueUpdate.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccIssueUpdate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({
                search_contains: true,
                enable_escape_special_char: false
            });
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
                                            <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName">Voucher No.</asp:Label>
                                            <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True"></asp:TextBox>
                                            <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ToolTip="You Can Change Voucher Number." ReadOnly="True"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4 pading5px">

                                            <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName"> Voucher Date</asp:Label>
                                            <asp:TextBox ID="txtdate" runat="server" CssClass="inputDateBox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                            </div>

                                        </div>

                                        <div class="col-md-3 pading5px pull-right">
                                            <div class="msgHandSt">
                                                <asp:Label ID="lblmsg" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </fieldset>
                         <asp:Panel ID="pnlBill" runat="server" Visible="False">
                           <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                   
                                    
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblBillList" runat="server" CssClass="lblTxt lblName" Text="Bill List"></asp:Label>
                                            <asp:TextBox ID="txtSrclsdno" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgbtnlsdno" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnlsdno_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>

                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlBillList" runat="server" CssClass="form-control inputTxt">
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
                            <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
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
                                            <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
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
                                            <asp:Label ID="lblAccdesc1" runat="server" Font-Size="11px"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "")   
                                                                        %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" ItemStyle-Font-Size="11px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Total" Visible="false" HeaderText="Rate"
                                        ItemStyle-Font-Size="11px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle ForeColor="Black" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFDrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="Black"
                                            HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCrAmt" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="Black"
                                            HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Memono" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemono" runat="server" CssClass="GridLebelL" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billid")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                            </asp:GridView>

                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-2 pading5px asitCol2 ">
                                            <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref. No"></asp:Label>
                                            <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                        </div>
                                        <div class="col-md-4 pading5px">

                                            <asp:Label ID="lblSrInfo" runat="server" CssClass="lblTxt lblName" Text="Other ref"></asp:Label>
                                            <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox"></asp:TextBox>




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


                                        <div class="colMdbtn pading5px">


                                            <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click">Final Update</asp:LinkButton>

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
