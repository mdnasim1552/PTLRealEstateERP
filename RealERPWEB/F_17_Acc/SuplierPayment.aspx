﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SuplierPayment.aspx.cs" Inherits="RealERPWEB.F_17_Acc.SuplierPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            margin: 0px 0 0 0px;
            font-size: 11px;
            font-weight: normal;
            border: solid 1px #006699;
            background-color: White;
        }

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });


          <%-- var gridview = $('#<%=this.dgv1.ClientID %>');
           $.keynavigation(gridview);--%>
        };


        function PrintRptCheque(vouanptype) {
            window.open('AccPrint.aspx?' + vouanptype + '', '_blank');

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

                                    <div class="col-md-1 pading5px">

                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName"> Voucher No.</asp:Label>
                                    </div>

                                    <div class="col-md-2 pading5px asitCol2">

                                        <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ReadOnly="True"></asp:TextBox>

                                    </div>
                                    <div class="col-md-2 pading5px asitCol2">

                                        <asp:Label ID="lblDate" runat="server" CssClass="smLbl_to" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtEntryDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>


                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lnkPrivVou" runat="server" CssClass="btn btn-primary" OnClick="lnkPrivVou_Click">Previous</asp:LinkButton>

                                        </div>
                                        <asp:TextBox ID="txtScrchPre" runat="server" CssClass="inputtextbox" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="ibtnFindPrv" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindPrv_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="colMdbtn pading5px pull-right">
                                            <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk_Click">Ok</asp:LinkButton>

                                        </div>



                                    </div>

                                    <div class="col-md-3 pading5px  asitCol3">
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlPrivousVou" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>


                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Control Code"></asp:Label>
                                        <%-- <asp:TextBox ID="txtScrchConCode" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindConCode" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindConCode_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>--%>
                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1 pading5px ">

                                        <asp:Label ID="Label2" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>

                                        <asp:CheckBox ID="chkPrint" runat="server" TabIndex="10" Text="Cheque Print" CssClass="btn btn-primary checkBox" />




                                    </div>

                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_B">

                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-1 pading5px ">
                                            <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">Supplier</asp:Label>
                                            <%--<asp:TextBox ID="txtserchReCode" runat="server" CssClass=" inputTxt inputName inpPixedWidth" TabIndex="14"></asp:TextBox>--%>


                                          <%--  <div class="colMdbtn">
                                                <asp:LinkButton ID="lnkRescode" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lnkRescode_Click" TabIndex="15"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>--%>

                                        </div>


                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlresuorcecode" runat="server" CssClass="chzn-select form-control inputTxt" TabIndex="17" AutoPostBack="true" OnSelectedIndexChanged="ddlresuorcecode_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>

                                        <div class="col-md-3 pading5px asitCol3">
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lnkAdd" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkAdd_Click">Add Bill</asp:LinkButton>


                                            </div>
                                        </div>




                                    </div>

                                    <div class="form-group">

                                        <div class="col-md-1 pading5px">
                                          <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" >Bill No</asp:Label>
                                              
                                           <%-- <asp:TextBox ID="txtserchBill" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>


                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lnkBillNo0" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lnkBillNo_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>--%>

                                        </div>
                                        
                                        <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" MaxDropDownHeight="200"
                                            TabIndex="8" TransitionalMode="True" Width="450px">
                                        </cc1:DropCheck>






                                    </div>



                                    <div class="form-group">



                                        <div class="col-md-1 Pading5px">
                                             <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>
                                        </div>


                                        <div class="col-md-11 pading5px  ">
                                           


                                            <asp:DropDownList ID="ddlcheque" runat="server" CssClass=" ddlPage" OnSelectedIndexChanged="ddlcheque_SelectedIndexChanged" AutoPostBack="true" Style="width: 72px;">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputtextbox"></asp:TextBox>





                                            <asp:Label ID="lblSrInfo" runat="server" CssClass=" smLbl_to" Text="Other ref"></asp:Label>
                                            <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:Label ID="lbltaxamt" runat="server" CssClass="smLbl_to" Style="width: 66px;">Tax</asp:Label>

                                            <asp:TextBox ID="txttaxamt" runat="server" CssClass=" inputtextbox" Style="text-align: right;"></asp:TextBox>

                                            <asp:Label ID="lblissueno" runat="server" Visible="False"></asp:Label>


                                            <asp:Label ID="lblPayto" runat="server" CssClass="smLbl_to">Pay To</asp:Label>

                                            <asp:TextBox ID="txtPayto" runat="server" CssClass=" inputtextbox" Width="120px" TabIndex="25" Font-Bold="true"></asp:TextBox>

                                            <cc1:AutoCompleteExtender ID="txtPayto_AutoCompleteExtender"
                                                runat="server" CompletionListCssClass="AutoExtender"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="15"
                                                DelimiterCharacters="" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="0" ServiceMethod="GetRecandPayDetails"
                                                ServicePath="~/AutoCompleted.asmx" TargetControlID="txtPayto">
                                            </cc1:AutoCompleteExtender>



                                            <%--      <asp:Label ID="lblPayto" runat="server" CssClass="smLbl" Text="Pay To"></asp:Label>
                                        <asp:TextBox ID="txtPayto" runat="server" CssClass="inputtextbox" ></asp:TextBox>--%>
                                        </div>



                                    </div>


                                </div>

                            </fieldset>
                        </asp:Panel>




                    </div>

                    <%--            <asp:DropDownList ID="ddlBillList" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>--%>

                    <div>

                        <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="685px">
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
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lnkTotal_Click">Total :</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc1" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                            Width="400px"></asp:Label>

                                        <asp:Label ID="lblAccdesc" runat="server"
                                            Visible="False"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Details Description" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpcldesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'
                                            Width="80px" TabIndex="78"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Quantity">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvQty" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" Style="text-align: right"
                                            ReadOnly="True" Width="60px"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                            TabIndex="79"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvRate" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Visible="False" Width="80px" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px" Font-Size="12px" ReadOnly="True" ForeColor="Black"
                                            Style="text-align: right" TabIndex="80"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr.Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="0px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                            TabIndex="81"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px" ReadOnly="True"
                                            Style="text-align: right" Width="90px"></asp:TextBox>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr.Amount" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                            TabIndex="82"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px" ReadOnly="True"
                                            Width="90px" ForeColor="White" Style="text-align: right"></asp:TextBox>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tax">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvtaxamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextbox"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taxam")).ToString("#,##0;(#,##0.); ") %>'
                                            Width="70px" Font-Size="12px" ForeColor="Black" TabIndex="78" Style="text-align: right;"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtaxamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px"
                                            Width="70px" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Net Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblgvnetamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextbox"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netam")).ToString("#,##0;(#,##0.); ") %>'
                                            Width="80px" Font-Size="12px" ForeColor="Black" TabIndex="78" Style="text-align: right;"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFnetamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px"
                                            Width="80px" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="80px" ForeColor="Black" TabIndex="83"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reconcilation" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrecndat" runat="server"
                                            Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                            Width="80px" TabIndex="78"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="RpCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrpcode" runat="server"
                                            Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode")) %>'
                                            Width="80px" TabIndex="60"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBillno" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextboxL" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="100px" ForeColor="Black" TabIndex="99"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>


                    <div>
                        <asp:Panel ID="pnlNarration" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <%-- <div class="form-group">
                                    <div class="col-md-2 pading5px asitCol2 ">
                                        <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>
                                        
                                         <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                   
                                       

                                    </div>
                                    <div class="col-md-7 pading5px">

                                        <asp:Label ID="lblSrInfo" runat="server" CssClass="lblTxt lblName" Text="Other ref"></asp:Label>
                                        <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox" ></asp:TextBox>
                                       
                                            <asp:Label ID="lblPayto" runat="server" CssClass="smLbl" Text="Pay To"></asp:Label>
                                        <asp:TextBox ID="txtPayto" runat="server" CssClass="inputtextbox" ></asp:TextBox>
                                    
                                             <asp:Label ID="lbltaxamt" runat="server" CssClass="smLbl_to" Style="width:66px;">Tax</asp:Label>

                                            <asp:TextBox ID="txttaxamt" runat="server" CssClass=" inputtextbox" Style="text-align:right;"></asp:TextBox>

                                            <asp:Label ID="lblissueno" runat="server" Visible="False"></asp:Label>
                                      

                                    </div>
                                                                    
                                  

                                   </div>--%>
                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3 pading5px">
                                            <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click">Final Update</asp:LinkButton>
                                            <a class=" btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=17")%>' style="margin: 0 0 0 5px;">Next</a>
                                            <asp:CheckBox ID="chkpost" runat="server" TabIndex="10" Text="post" CssClass="btn btn-primary checkBox" Visible="false" />
                                        </div>


                                        <div class="col-md-2 pading5px">

                                            <asp:Label ID="lblisunum" runat="server" CssClass=" smLbl" Visible="False"></asp:Label>
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

