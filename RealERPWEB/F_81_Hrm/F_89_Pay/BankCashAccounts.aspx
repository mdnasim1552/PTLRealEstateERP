<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BankCashAccounts.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.BankCashAccounts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        
       
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <link href="../Content/chosen.css" rel="stylesheet" />
    <script src="../Scripts/chosen.jquery.js"></script>

     <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('#<%=this.txtScrchConCode.ClientID %>').focus();

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


            var gridview = $('#<%=this.dgv1.ClientID %>');
            $.keynavigation(gridview);
            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });
            //$(".chzn-select").chosen();
            //$(".chzn-select-deselect").chosen({ allow_single_deselect: true });


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

                                    <div class="col-md-4 pading5px  asitCol4">
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName"> Voucher No.</asp:Label>
                                        <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ReadOnly="True"></asp:TextBox>
                                        <asp:Label ID="lblDate" runat="server" CssClass="smLbl" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtEntryDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>


                                    </div>

                                    <div class="col-md-6 pading5px">


                                        <asp:LinkButton ID="lnkPrivVou" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lnkPrivVou_Click">Previous</asp:LinkButton>


                                        <asp:TextBox ID="txtScrchPre" runat="server" CssClass="inputtextbox" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindPrv" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindPrv_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                        <asp:DropDownList ID="ddlPrivousVou" runat="server" CssClass=" ddlistPull" Style="width: 193px;">
                                        </asp:DropDownList>



                                    </div>




                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Control Code"></asp:Label>
                                        <asp:TextBox ID="txtScrchConCode" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindConCode" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindConCode_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="form-control inputTxt chzn-select  " OnSelectedIndexChanged="ddlConAccHead_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-2 pading5px">
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk_Click">Ok</asp:LinkButton>

                                        </div>
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn primaryBtn disabled" runat="server" Style="margin-left: 10px;" Visible="false"></asp:Label>
                                        </div>






                                    </div>

                                </div>
                            </div>

                        </fieldset>





                    </div>

                    <div class="row">
                        <div class="col-md-5 pading5px">
                            <asp:Panel ID="Panel2" runat="server" Visible="False">
                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">

                                        <div class="form-group">
                                            <div class="col-md-12 pading5px">
                                                <asp:Label ID="lblactcode" runat="server" CssClass="lblTxt lblName">Month</asp:Label>
                                               
                                                 <asp:DropDownList ID="ddlMonth" runat="server" CssClass=" ddlPage" TabIndex="13" style="width:150px;">
                                                </asp:DropDownList>
                                                 <asp:LinkButton ID="lnkAdd" runat="server" CssClass="btn btn-primary  okBtn" OnClick="llnkAdd_Click" TabIndex="21">Add</asp:LinkButton>

                                            </div>
                                        </div>

                                     
                                    </div>


                                </fieldset>

                            </asp:Panel>
                            <div class="spacing"></div>


                            <asp:Panel ID="pnlNarration" runat="server" Visible="False">
                                <fieldset class="scheduler-border fieldset_Nar">
                                    <div class="form-horizontal">

                                        <div class="form-group">
                                            <div class="col-md-2 pading5px asitCol2 ">
                                                <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Cheque No"></asp:Label>
                                                <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                            </div>
                                            <div class="col-md-6 pading5px">

                                                <asp:Label ID="lblSrInfo" runat="server" CssClass=" lblTxt lblName" Text=" Referecne No"></asp:Label>
                                                <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox" Width="130px"></asp:TextBox>


                                            </div>




                                        </div>
                                        <div class="form-group">

                                             <div class="col-md-2 pading5px asitCol2 ">

                                            <asp:Label ID="lblchelist" runat="server" CssClass="lblTxt lblName" Text="Cheque List"></asp:Label>
                                                
                                             <asp:DropDownList ID="ddlcheque" runat="server" CssClass=" ddlPage" OnSelectedIndexChanged="ddlcheque_SelectedIndexChanged" AutoPostBack="true" style="width:72px;">
                                                  
                                                </asp:DropDownList>    
                                                 </div> 
                                           


                                            <div class="col-md-6 pading5px">

                                                <asp:Label ID="lblPayto" runat="server" CssClass="lblTxt lblName" Text="Pay To"></asp:Label>
                                                <asp:TextBox ID="txtPayto" runat="server" CssClass="inputtextbox" Width="130"></asp:TextBox>
                                                   <cc1:AutoCompleteExtender ID="txtPayto_AutoCompleteExtender"
                                                runat="server" CompletionListCssClass="AutoExtender"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="15"
                                                DelimiterCharacters="" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="0" ServiceMethod="GetRecandPayDetails"
                                                ServicePath="~/AutoCompleted.asmx" TargetControlID="txtPayto">
                                            </cc1:AutoCompleteExtender>



                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-12 pading5px">
                                                <div class="input-group">
                                                    <span class="input-group-addon glypingraddon">
                                                        <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                    </span>
                                                    <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="2" Width="345" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>


                                        </div>




                                    </div>
                                </fieldset>

                                <div class="row">

                                    <div class=" col-md-offset-1 col-md-11   pading5px">
                                        <a class="btn btn-primary primaryBtn" target="_blank" href='<%=this.ResolveUrl("AccCodeBook.aspx?InputType=Accounts")%>' style="margin: 0 5px;">Account's Code</a>

                                        <a class="btn btn-primary primaryBtn" target="_blank" href='<%=this.ResolveUrl("AccSubCodeBook.aspx?InputType=ResCodePrint")%>' style="margin: 0 0 0 5px;">Details Code</a>
                                        <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click" Style="margin: 0 5px;">Final Update</asp:LinkButton>

                                        <asp:CheckBox ID="chkPrint" runat="server" TabIndex="10" Text="Cheque Print" CssClass="btn btn-primary checkBox" />
                                        <a class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=17")%>' style="margin: 0 0 0 5px; line-height: 18px;">Back</a>

                                        <asp:Label ID="lblisunum" runat="server" CssClass=" smLbl" Visible="False"></asp:Label>
                                        <asp:CheckBox ID="chkpost" runat="server" TabIndex="10" Text="post" CssClass="btn btn-primary checkBox" Visible="false" />





                                    </div>
                                </div>
                            </asp:Panel>

                        </div>

                        <div class="col-md-7 pading5px">

                            <div class="table-responsive">

                                <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea "
                                    ShowFooter="True" Width="665px" OnRowDataBound="dgv1_RowDataBound" OnRowDeleting="dgv1_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="15px">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-Width="30px">
                                        <ControlStyle Width="30px" />
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle Width="30px" />
                                        </asp:CommandField>
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
                                        <asp:TemplateField HeaderText="Head of Accounts" HeaderStyle-Width="230px">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkTotal" runat="server" Font-Bold="True"
                                                    OnClick="lnkTotal_Click" CssClass="btn btn-primary primarygrdBtn pull-right">Total :</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>



                                                <asp:HyperLink ID="hlnkAccdesc1" runat="server" Target="_blank" Font-Size="10px"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") +                                                                           
                                                                        " &nbsp;&nbsp;&nbsp;&nbsp;"+Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                                    Width="230px" Font-Names="Verdana"></asp:HyperLink>

                                                <asp:Label ID="lblAccdesc" runat="server"
                                                    Font-Size="11px" Visible="False"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="50px" Font-Names="Verdana"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Details Description" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResdesc" runat="server"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpcldesc" runat="server"
                                                    Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'
                                                    Width="80px" TabIndex="78"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="50px">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvQty" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Visible="False" Width="40px" Font-Size="12px" Style="text-align: right"
                                                    ReadOnly="True"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="40px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                    TabIndex="79"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="50px">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvRate" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Visible="False" Width="50px" Font-Size="12px" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px" Font-Size="12px" ReadOnly="True" ForeColor="Black"
                                                    Style="text-align: right" TabIndex="80"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dr.Amount" HeaderStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="0px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                    TabIndex="81"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" ForeColor="Black"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="12px" ReadOnly="True"
                                                    Width="70px" Style="text-align: right"></asp:TextBox>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr.Amount" HeaderStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                    TabIndex="82"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent" ForeColor="Black"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="12px" ReadOnly="True"
                                                    Width="70px" Style="text-align: right"></asp:TextBox>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                    Width="80px" ForeColor="Black" TabIndex="83"></asp:Label>
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
                                        <asp:TemplateField HeaderText="Bill No" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBillno" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    CssClass="GridTextboxL" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                    Width="100px" ForeColor="Black" TabIndex="99"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                            <asp:Label ID="lblInword" runat="server" CssClass="lblTxt lblName"  Style="width:600px; color:blue; text-align:left;"></asp:Label>
                        </div>



                    </div>





                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->
            <script src="../AngularJs/AgControlerData.js"></script>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

