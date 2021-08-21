<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptBillAdjStatus.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptBillAdjStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $('#<%=this.txtrcvDate.ClientID %>').focus();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);




        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.TxtPenBillStatus(event);



            });


            var gv1 = $('#<%=this.gvPayment.ClientID %>');
            gv1.Scrollable();




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
                                <asp:Panel ID="pnlmain" runat="server">

                                    <div class="form-group">
                                        <div class="col-md-12 ">

                                            <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="Receive Date:"></asp:Label>
                                            <asp:TextBox ID="txtfrmdate" runat="server" AutoPostBack="True"
                                                CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>


                                            <asp:Label ID="lblfrmdate0" runat="server" CssClass="lblTxt lblName" Text="To" Width="130"></asp:Label>
                                            <asp:TextBox ID="txttoDate" runat="server" AutoPostBack="True"
                                                CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12 ">
                                            <asp:Label ID="lblRcvDate" runat="server" CssClass="lblTxt lblName">Received Date</asp:Label>
                                            <asp:TextBox ID="txtrcvDate" runat="server" OnTextChanged="txtrcvDate_TextChanged" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:Label ID="lblAppadjdate" runat="server" CssClass="lblTxt lblName" Width="130px">Adjustment Date</asp:Label>
                                            <asp:DropDownList ID="ddladjdate" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddladjdate_SelectedIndexChanged" CssClass="ddlPage"
                                                TabIndex="2">
                                                <asp:ListItem Value="=">Equal</asp:ListItem>

                                                <asp:ListItem Value="between">Between</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtadjdate" runat="server" CssClass="inputtextbox" OnTextChanged="txtadjdate_TextChanged"></asp:TextBox>

                                            <asp:Label ID="lblapppdateto" runat="server" CssClass=" smLbl_to" Visible="false">To</asp:Label>

                                            <asp:TextBox ID="txtadjdateto" runat="server" AutoPostBack="True" CssClass="inputtextbox"
                                                OnTextChanged="txtadjdateto_TextChanged" TabIndex="4"
                                                Visible="False"></asp:TextBox>
                                            <div class=" clearfix"></div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblRefnum" runat="server" CssClass="lblTxt lblName" Text="Ref Number:"></asp:Label>
                                            <asp:TextBox ID="txtRefnum" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:Label ID="lblBillNature" runat="server" CssClass="lblTxt lblName" Text="Nature Of Bill:" Width="130"></asp:Label>
                                            <asp:TextBox ID="txtnofbill" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                            <div class=" clearfix"></div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblProjectName" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>
                                            <asp:TextBox ID="txtProjectName" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:Label ID="lblpartyName" runat="server" CssClass="lblTxt lblName" Text="Party Name:" Width="130"></asp:Label>
                                            <asp:TextBox ID="txtpartyName" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                            <div class=" clearfix"></div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblChqRDate" runat="server" CssClass="lblTxt lblName" Text="Chq. Ready Date:"></asp:Label>
                                            <asp:TextBox ID="txtchqrdate" runat="server" OnTextChanged="txtchqrdate_TextChanged" TabIndex="9" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:Label ID="lblchqptpdate" runat="server" CssClass="lblTxt lblName" Text="Payment To Party:" Width="130"></asp:Label>
                                            <asp:TextBox ID="txtcptpdate" runat="server" AutoPostBack="True" OnTextChanged="txtcptpdate_TextChanged" TabIndex="10" CssClass="inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="lnkOk" runat="server" OnClick="lnkOk_Click" TabIndex="20" Text="Ok" CssClass="btn btn-primary okBtn"></asp:LinkButton>
                                            <div class=" clearfix"></div>
                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <div class="col-md-12">
                                            <asp:Label ID="lblBillAmount" runat="server" CssClass="lblTxt lblName" Text="Bill Amount:"></asp:Label>
                                            <asp:TextBox ID="txtBillamount" runat="server" TabIndex="9" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:Label ID="lblIssueNo" runat="server" CssClass="lblTxt lblName" Text="Issue No:" Width="130"></asp:Label>
                                            <asp:TextBox ID="txtissueno" runat="server" TabIndex="12" CssClass="inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="lnkRefresh" runat="server" OnClick="lnkRefresh_Click" TabIndex="50" Text="Refresh" CssClass="btn btn-primary okBtn" Width="80"></asp:LinkButton>

                                            <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                            <div class=" clearfix"></div>
                                        </div>
                                    </div>


                                </asp:Panel>

                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                             <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Style="margin-top: 0px" Width="831px">

                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue #">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvrcvdate" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Nature">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillnature" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ref #">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvref" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFTotal" runat="server" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvgvbillamt" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Appx.</br>Adjustment Date">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvAppdate" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "appadjdate")) %>'
                                            Width="80px"></asp:Label>
                                       
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Adjustment Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAdjustdate" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "adjdate")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "adjdate")).ToString("dd-MMM-yyyy")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRemarks" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                                <%--<asp:TemplateField HeaderText="Total Day's(Acct. Dept)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtodays" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "todays")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>--%>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </div>
            </div>

             
                   

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

