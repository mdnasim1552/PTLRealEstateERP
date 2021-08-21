<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccGenAdjJournal.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccGenAdjJournal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);



        });


        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
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
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class=" col-md-4  pading5px asitCol4">

                                            <asp:Label ID="lblDate" CssClass="lblTxt lblName" runat="server" Text="Voucher Date"></asp:Label>

                                            <asp:TextBox ID="txtdate" runat="server" CssClass=" inputtextbox"
                                                TabIndex="1"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                            
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click"
                                                Text="Ok" TabIndex="2"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class=" col-md-5  pading5px ">

                                            <asp:Label ID="lblcurVounum" CssClass="lblTxt lblName" runat="server" Font-Size="10px" Text="Current Voucher No"></asp:Label>
                                            <asp:LinkButton ID="ibtnvounu" runat="server" CssClass="btn btn-primary srearchBtn" Visible="False" OnClick="ibtnvounu_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            <asp:TextBox ID="txtcurrentvou" runat="server" CssClass=" inputtextbox" AutoPostBack="true" ReadOnly="True"
                                                TabIndex="3"></asp:TextBox>
                                            <asp:TextBox ID="txtCurrntlast6" AutoPostBack="true" runat="server" CssClass=" smltxtBox60px" ReadOnly="True"
                                                TabIndex="4">.</asp:TextBox>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol3">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <asp:Panel ID="pnlBill" runat="server" Visible="False">
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class=" col-md-3  pading5px asitCol3">

                                            <asp:Label ID="lblbillno" CssClass="lblTxt lblName" runat="server" Text="Bill No:"></asp:Label>
                                            <asp:TextBox ID="txtSrchbillno" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgSearchProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchProject_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        </div>


                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlbillno" runat="server" CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>


                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lbtnSelec" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelec_Click">Select</asp:LinkButton>

                                        </div>

                                    </div>
                                  <%--  <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblUnit" runat="server" CssClass="lblTxt lblName">Unit Name</asp:Label>
                                            <asp:TextBox ID="txtSrchUnit" runat="server" CssClass=" inputtextbox" TabIndex="8"></asp:TextBox>

                                            <asp:LinkButton ID="imgSearchUnit" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchUnit_Click" TabIndex="15"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlUnitName" runat="server"
                                                CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    
                                </div>
                            </fieldset>

                            <div class=" table table-responsive">
                                <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" 
                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Style="text-align: left" Width="685px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       

                                     <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtndelete" runat="server"  OnClick="lbtndelete_Click"><span class="glyphicon glyphicon-remove"> </span></asp:LinkButton>
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
                                                <asp:Label ID="lblAccdesc1" runat="server" Font-Size="11px"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")   
                                                                        %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="11px" />


                                           <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server"
                                            OnClick="lbtnTotal_Click" CssClass="btn btn-primary primaryBtn">Total</asp:LinkButton>
                                    </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFDrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblgvDrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" Style="text-align:right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:TextBox>
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
                                                <asp:TextBox ID="lblgvCrAmt" runat="server" BackColor="Transparent" BorderStyle="None" Style="text-align:right;"
                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="11px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#000"
                                                HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillno11" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                         <%--            <asp:TemplateField HeaderText="Billno" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillno1" runat="server" CssClass="GridLebelL" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                    <%-- <FooterStyle BackColor="#0066CC" />
                                    <HeaderStyle BackColor="#0066CC" BorderStyle="Solid" BorderWidth="2px" 
                                        Font-Bold="True" ForeColor="#000" />
                                    <AlternatingRowStyle BackColor="#FFCCFF" />--%>
                                </asp:GridView>

                            </div>

                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="lblRefNum" CssClass="lblTxt lblName" Font-Size="11px" runat="server" Text="Ref./Cheq No/Slip No." Width="120px"></asp:Label>
                                        <asp:TextBox ID="txtRefNum" runat="server" CssClass=" inputtextbox" AutoCompleteType="Disabled"></asp:TextBox>

                                    </div>
                                    <div class=" col-md-3 pading5px asitCol3">

                                        <asp:Label ID="lblSrInfo" CssClass="lblTxt lblName" runat="server" Text="Other ref.(if any)" Width="120px"></asp:Label>
                                        <asp:TextBox ID="txtSrinfo" runat="server" CssClass=" inputtextbox" AutoCompleteType="Disabled"></asp:TextBox>

                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click">Final Update</asp:LinkButton>


                                       

                                    </div>

                                 
                                    <div class=" clearfix"></div>
                                </div>
                                <div class="form-group">
                                   
                                    <div class="col-md-8 pading5px">
                                        <asp:Label ID="lblNaration" runat="server"  CssClass="lblTxt lblName" Text="Narration"
                                            Width="120px"></asp:Label>

                                         <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled" CssClass="form-control pull-left"
                                            TextMode="MultiLine" Width="500px"></asp:TextBox>
                                    </div>

                                            <asp:Label ID="lblisunum" runat="server" CssClass=" smLbl" Visible="False"></asp:Label>                                           
                                          <asp:CheckBox ID="chkpost" runat="server" TabIndex="10" Text="post" CssClass="btn btn-primary checkBox" Visible="false" />
                                </div>
                            </div>


                       
                        </div>
                    </asp:Panel>
                </div>
            </div>

                    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

