<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSupplierBillDetails.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptSupplierBillDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script type="text/javascript" language="javascript">

              $(document).ready(function () {

                  Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

              });

              function pageLoaded() {

                  $('.chzn-select').chosen({ search_contains: true });


                  $('#<%=this.chkasondate.ClientID%>').change(function () {

                      try {

                          var chk = $(this).is(':checked');
                          if (chk == true)
                          {
                              $('#<%=this.txtDate.ClientID%>').hide();
                              $('#<%=this.lblfrmdate.ClientID%>').hide();

                              $('#<%=this.lbltodate.ClientID%>').text("Date:");

                          }
                          else
                          {
                              $('#<%=this.txtDate.ClientID%>').show();
                              $('#<%=this.lblfrmdate.ClientID%>').show();
                              $('#<%=this.lbltodate.ClientID%>').text("To:");

                          }
                         
                      
                        
                      }
                      catch (e)
                      {

                          alert(e.message);

                      }

                  });


                 }




    </script>

       <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
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

                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label16" runat="server" CssClass="lblTxt lblName" Font-Size="11px " Text="Supplier Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcSub" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindSubConName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindSubConName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>

                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlSubName" runat="server" Style="width: 336px;"
                                            TabIndex="2" AutoPostBack="True" CssClass="chzn-select ddlistPull"
                                            OnSelectedIndexChanged="ddlSubName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-1 asitCol1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn"
                                            TabIndex="6">Ok</asp:LinkButton>
                                    </div>
                                    <cc1:ListSearchExtender ID="ddlSubName_ListSearchExtender" runat="server"
                                        QueryPattern="Contains" TargetControlID="ddlSubName">
                                    </cc1:ListSearchExtender>

                                </div>


                            </div>
                            <div class="form-group">
                                <div class="col-md-3 asitCol3 pading5px">
                                    <asp:Label ID="Label17" runat="server" CssClass="lblTxt lblName"
                                        Text="Project Name:"></asp:Label>

                                    <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"
                                        TabIndex="3"></asp:TextBox>
                                    <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                </div>
                                <div class="col-md-4 asitCol4 pading5px">
                                    <asp:DropDownList ID="ddlProjectName" runat="server"
                                        CssClass="chzn-select ddlistPull" Width="336px" TabIndex="5"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>


                                <div class="col-md-3">
                                    <asp:Label ID="lblfrmdate" runat="server" CssClass="smLbl_to" Text="From:"></asp:Label>

                                    <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox"
                                        TabIndex="7"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender0" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                    <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>

                                    <asp:TextBox ID="txttoDate" runat="server" CssClass="inputtextbox"
                                        TabIndex="7"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                                </div>


                                <div class="col-md-2">
                                    <label id="chkbod" runat="server" class="switch">
                                        <asp:CheckBox ID="chkasondate" runat="server" />
                                        <span class="btn btn-xs slider round"></span>
                                    </label>
                                    <asp:Label runat="server" Text="as on date" ID="lblnetbalance" CssClass="control-label"></asp:Label>

                                </div>



                            </div>
                    </div>
                    </fieldset>


                </div>



                <div class="table table-responsive">
                    <asp:GridView ID="gvSubBill" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True">
                        <PagerSettings Position="Top" />
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPrjName" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="130px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <%-- <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrderno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "order1")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Bill No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvBillno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ref.Bill</br> No">
                                <FooterTemplate>
                                    <asp:Label ID="torlal" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black">Total</asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvbillref" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Chalan No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvchnno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clnno")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bill Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbildate" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="65px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbillamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFBillAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                </FooterTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sec %">
                                <ItemTemplate>
                                    <asp:Label ID="lgvpercntge" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SD Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbillamt1" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFSecurityAmt" runat="server" Font-Bold="True"
                                        Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deduction">
                                <ItemTemplate>
                                    <asp:Label ID="lgvdedamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFdedAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Penalty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvpenaltyamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "penamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFPenaltyAmt" runat="server" Font-Bold="True"
                                        Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total net amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbillamt3" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFTotalAmount" runat="server" Font-Bold="True"
                                        Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Payment">
                                <ItemTemplate>
                                    <asp:Label ID="lblpayment" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFPayment" runat="server" Font-Bold="True"
                                        Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText=" Security Deposit Payment">
                                <ItemTemplate>
                                    <asp:Label ID="lblspayment" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "spayment")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFsPayment" runat="server" Font-Bold="True"
                                        Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="Net Payable">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbillamt2" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayable")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFNetpayableAmt" runat="server" Font-Bold="True"
                                        Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>
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

