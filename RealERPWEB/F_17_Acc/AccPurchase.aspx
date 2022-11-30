
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccPurchase.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccPurchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

         <%--  var gridview = $('#<%=this.dgv2.ClientID %>');
           $.keynavigation(gridview);--%>
        };

        function Confirmation() {
            if (confirm('Are you sure you want to save?')) {
                return;
            } else {
                return false;
            }
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
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName"> Voucher No.</asp:Label>
                                        <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ReadOnly="True"></asp:TextBox>

                                    </div>
                                    <div class="col-md-4 pading5px">

                                        <asp:Label ID="lblDate" runat="server" CssClass="smLbl " Text="Date"></asp:Label>
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

                                            <asp:CheckBox ID="chkBill" runat="server" TabIndex="10" Text="Bill Print" CssClass="btn btn-primary checkBox" />
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
                                        <asp:Label ID="lblsuplist" runat="server" CssClass="lblTxt lblName " Text="Supplier"></asp:Label>
                                        <asp:TextBox ID="txtSupSearch" runat="server" CssClass=" inputtextbox" TabIndex="4"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImgbtnFindSup" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSup_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSupList" runat="server" AutoPostBack="True"
                                            Font-Size="12px" OnSelectedIndexChanged="ddlSupList_SelectedIndexChanged"
                                            TabIndex="9" CssClass="chzn-select form-control  inputTxt">
                                        </asp:DropDownList>


                                    </div>
                                    </div>


                                
                                 <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblBillList" runat="server" CssClass="lblTxt lblName" Text="Bill List"></asp:Label>
                                            <asp:TextBox ID="txtBillno" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgSearchBillno" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchBillno_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>


                                      <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" CssClass=" "
                                                MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True" Width="350px">
                                            </cc1:DropCheck>

                                     <asp:LinkButton ID="lbtnSelectBill" runat="server" CssClass="btn btn-primary primaryBtn" 
                                         style="margin:-20px 0 0 350px; float:left !important;"   OnClick="lbtnSelectBill_Click">Select</asp:LinkButton>
                                      

                                    </div>
                                </div>
                            </fieldset>

                        </asp:Panel>
                           
                    </div>

                    <div class="row">
                        <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False"
                            CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="689px" OnRowDataBound="dgv2_RowDataBound">
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


                                
                                <asp:TemplateField HeaderText="Specification">
                                    <%--<FooterTemplate>
                                        <asp:LinkButton ID="lbtnFinalUpdate" runat="server"  OnClick="lbtnFinalUpdate_Click"  CssClass="btn btn-danger primarygrdBtn">Final Update </asp:LinkButton>
                                    </FooterTemplate>--%>

                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlspcfdesc" CssClass=" ddlPage125 chzn-select" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                       
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Quantity"
                                    ItemStyle-Font-Size="11px">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvQty" runat="server" ReadOnly="true"
                                            Visible="False" Width="70px"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty" runat="server" ReadOnly="True"  BackColor="Transparent"
                                              BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server"
                                            OnClick="lbtnTotal_Click" CssClass="btn btn-primary primaryBtn">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRate" runat="server" ReadOnly="True"  BackColor="Transparent"
                                              BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle ForeColor="White" />
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvDrAmt" runat="server" ReadOnly="true" BackColor="Transparent"   BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                           
                                            Width="70px"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvDrAmt" runat="server"  BackColor="Transparent"   BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvCrAmt" runat="server"  ReadOnly="true" BackColor="Transparent"   BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Width="70px"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"   BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
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

                                <asp:TemplateField HeaderText="Narration">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNarration" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billnar")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Billno" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillno" runat="server" CssClass="GridLebelL" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>

                    </div>
                    <div class="row">
                         <asp:Panel ID="PnlNarration" runat="server" Visible="False">
                       <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3 ">
                                        <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>
                                        
                                         <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                   
                                       

                                    </div>
                                    <div class="col-md-8 pading5px">

                                        <asp:Label ID="lblSrInfo" runat="server" CssClass="lblTxt lblName" Text="Other ref.(if any)"></asp:Label>
                                        <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox" ></asp:TextBox>
                                       
                                         <asp:Label ID="lblAdvanced" runat="server" CssClass="lblTxt lblName" Text="Advanced"></asp:Label>
                                         <asp:TextBox ID="txtAdvanced" runat="server" CssClass="inputTxt inpPixedWidth text-right" AutoCompleteType="Disabled" TabIndex="4"></asp:TextBox>
                                      <asp:CheckBox ID="chkpost" runat="server" TabIndex="10" Text="post" CssClass="btn btn-primary checkBox" Visible="false" />

                                        <asp:HyperLink ID="lbtnBalance" runat="server" Target="_blank" Style="margin-left:10px; color:blue; font-weight:bold; font-size:14px;" ></asp:HyperLink>
                                       
                                         <asp:Label ID="lblpayto" runat="server" CssClass="lblTxt lblName" Text="Pay To :"></asp:Label>
                                        
                                         <asp:TextBox ID="txtPayto" runat="server" CssClass="inputtextbox" Width="180px"></asp:TextBox>

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
                                        
                                         
                                         <div class="col-md-3 pading5px">
                                            <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClientClick="return Confirmation();" OnClick="lnkFinalUpdate_Click" >Update</asp:LinkButton>
                                             <a class=" btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=17")%>' style="margin:0 0 0 5px;">Next</a>
                                        </div>
                                        
                                       

                                    
                                    </div>
                                    </div>

                            </fieldset>
                         </asp:Panel>
                      </div>

                    </div>

                </div>
            </div>
           
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


